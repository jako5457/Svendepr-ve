using Duende.IdentityServer;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
            }

            if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            builder.Services.AddCors(o => { 
                o.AddDefaultPolicy(b => { 
                            b.WithOrigins("https://localhost:7039")
                                .AllowAnyMethod()
                                .AllowAnyHeader(); 
                });
                o.AddDefaultPolicy(b => {
                    b.WithOrigins("https://svendproveapi.azurewebsites.net")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.Endpoints.EnableAuthorizeEndpoint = true;
                    options.Endpoints.EnableIntrospectionEndpoint = true;
                    // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddTransient<IEmailSender, EmailSender>(i => 
                new EmailSender(
                    builder.Configuration["EmailSender:host"],
                    builder.Configuration.GetValue<int>("EmailSender:port"),
                    builder.Configuration.GetValue<bool>("EmailSender:enableSSL"),
                    builder.Configuration["EmailSender:userName"],
                    builder.Configuration["EmailSender:password"]
                    )
            );

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (app.Environment.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }
    }
}
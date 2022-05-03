using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using WebClient.Helpers.Api;
using WebClient.Helpers.Constants;

var builder = WebApplication.CreateBuilder(args);


#region ConfigureServices

builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddMemoryCache();

builder.Services.AddMvc();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "Admin"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "User"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = IdentityConst.IdentityServer;

        options.ClientId = "web";
        options.ClientSecret = "websecret";
        options.ResponseType = "code";

        #region Scopes
            //options.Scope.Clear();
            options.Scope.Add("company_read");
            options.Scope.Add("company_write");
            options.Scope.Add("driver_read");
            options.Scope.Add("driver_write");
            options.Scope.Add("employee_read");
            options.Scope.Add("employee_write");
            options.Scope.Add("order_read");
            options.Scope.Add("order_write");
            options.Scope.Add("product_read");
            options.Scope.Add("product_write");
            options.Scope.Add("product_request_read");
            options.Scope.Add("product_request_write");
            options.Scope.Add("warehouse_read");
            options.Scope.Add("warehouse_write");
            //options.Scope.Add("api_all_read");
            //options.Scope.Add("api_all_write");
            options.Scope.Add("offline_access"); //Client Request AccesToken
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("Mailverf");
        #endregion


        options.ClaimActions.MapJsonKey(JwtClaimTypes.Role, JwtClaimTypes.Role);
        options.GetClaimsFromUserInfoEndpoint = true; //User Information
        options.SaveTokens = true; //Client saves AccessToken

    });

builder.Services.AddHttpClient();

builder.Services.AddTransient<IApiCaller, ApiCaller>();
#endregion

// Add services to the container.

var app = builder.Build();

#region Configure

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();

#endregion


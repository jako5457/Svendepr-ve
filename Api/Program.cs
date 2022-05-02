using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var IdentityEndpoint = builder.Configuration.GetValue<string>("IdentityEndpoint");

builder.Services.AddSwaggerGen(options =>
{
    Dictionary<string,string> scopes = new Dictionary<string, string>();

    scopes.Add("openid", "OpenId Connect");
    scopes.Add("profile", "Profile");
    #region Swagger scopes
    scopes.Add("company_read", "read company data");
    scopes.Add("company_write", "Edit/create company data");
    scopes.Add("driver_read", "Read driver information");
    scopes.Add("driver_write", "Edit/create driver information");
    scopes.Add("employee_read", "Read employees");
    scopes.Add("employee_write", "edit/create employees");
    scopes.Add("order_read", "Read order information");
    scopes.Add("order_write", "Edit/create order information");
    scopes.Add("product_read", "Read product information");
    scopes.Add("product_write", "Edit/create product");
    scopes.Add("product_request_read", "Read order requests");
    scopes.Add("product_request_write", "Edit/create order requests");
    scopes.Add("warehouse_read", "Read warehouse information");
    scopes.Add("warehouse_write", "Edit/create warehouse information");
    #endregion Swagger scopes

    var scheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{IdentityEndpoint}/connect/authorize"),
                TokenUrl = new Uri($"{IdentityEndpoint}/connect/token"),
                Scopes = scopes
            }
        },
        Type = SecuritySchemeType.OAuth2
    };

    options.AddSecurityDefinition("OAuth", scheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "OAuth", Type = ReferenceType.SecurityScheme }
            },
            new List<string> { }
        }
    });
});

builder.Services.AddDbContext<ApiDbcontext>(b => b.UseSqlServer(builder.Configuration.GetConnectionString("ApiDatabase")));
//builder.Services.AddDbContext<ApiDbcontext>(b => b.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.Authority = IdentityEndpoint;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

builder.Services.AddCors(o => {
    o.AddDefaultPolicy(b => {
        b.WithOrigins("https://svendproveapi.azurewebsites.net")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthScopes("openid", "profile");
        c.OAuthClientId("Api");
        c.OAuthClientSecret("Apisecret");
        c.OAuthUsePkce();
    });
}

if (app.Environment.IsProduction())
{
    app.UseSwaggerUI(c =>
    {
        c.OAuthScopes("openid", "profile");
        c.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

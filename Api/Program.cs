using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

string endpoint = "https://localhost:5001";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    Dictionary<string,string> scopes = new Dictionary<string, string>();

    scopes.Add("openid", "openid");
    scopes.Add("profile", "profile");

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

    var scheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{endpoint}/connect/authorize"),
                TokenUrl = new Uri($"{endpoint}/connect/token"),
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

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.Authority = endpoint;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("web");
        c.OAuthClientSecret("websecret");
        c.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

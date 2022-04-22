using System.IdentityModel.Tokens.Jwt;
using WebClient.Helpers.Api;

var builder = WebApplication.CreateBuilder(args);


#region ConfigureServices

builder.Services.AddRazorPages();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://identityserversvende.azurewebsites.net/";

        options.ClientId = "web";
        options.ClientSecret = "websecret";
        options.ResponseType = "code";

        //options.Scope.Clear();
        options.Scope.Add("offline_access"); //Client Request AccesToken
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("Api1");
        options.GetClaimsFromUserInfoEndpoint = true; //User Information
        options.SaveTokens = true; //Client saves AccessToken

    });

builder.Services.AddHttpClient();

builder.Services.AddScoped<IApiCaller, ApiCaller>();
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();

#endregion


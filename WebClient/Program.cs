using System.IdentityModel.Tokens.Jwt;

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
        options.Authority = "https://10.135.16.153:5001";

        options.ClientId = "WebRazor";
        options.ClientSecret = "ThisIsASecretRazor";
        options.ResponseType = "code";
        

        options.SaveTokens = true;
        options.Scope.Add("offline_access"); //Client Request AccesToken
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("Api1");
        options.GetClaimsFromUserInfoEndpoint = true; //User Information
        options.SaveTokens = true; //Client saves AccessToken

        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        options.BackchannelHttpHandler = handler;
    });


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

app.UseEndpoints(endpoins =>
{
    endpoins.MapDefaultControllerRoute()
    .RequireAuthorization();
});

app.MapRazorPages();

app.Run();

#endregion


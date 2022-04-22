using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { new ApiScope("Api1", "My API") };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"Api1"}
            },
            new Client
            {
                ClientId = "web",
                ClientSecrets = {new Secret("websecret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                // where to redirect to after login
                RedirectUris = { "https://localhost:7039/swagger/oauth2-redirect.html" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Api1"
                }

            }
        };
    }
}
//new Client[]
//{
//    // This would be an example off adding our api, therefore machine to machine
//    // m2m client credentials flow client
//    new Client
//    {
//        ClientId = "m2m.client",
//        ClientName = "Client Credentials Client",

//        AllowedGrantTypes = GrantTypes.ClientCredentials,
//        ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

//        AllowedScopes = { "scope1" }
//    },
//    //Here i configure our client WebRazor.
//    new Client
//    {
//        ClientId = "WebRazor",
//        ClientSecrets = { new Secret("ThisIsASecretRazor".Sha256())},

//        AllowedGrantTypes = GrantTypes.Code,

//        RedirectUris = { "https://localhost:44300/signin-oidc" },
//        FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
//        PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

//        //Giver Clienten en AccessToken.
//        AllowOfflineAccess = true,

//        AllowedScopes = new List<string>
//        {
//            IdentityServerConstants.StandardScopes.OpenId,
//            IdentityServerConstants.StandardScopes.Profile,
//            "Api1"
//        }
//    },
//    new Client
//    {
//        ClientId = "Xamarin",
//        ClientName = "TrackingAPP",
//        AllowedGrantTypes = GrantTypes.Code,
//        ClientSecrets = { new Secret("ThisIsAXamarinSecret".Sha256()) },
//        AllowOfflineAccess = true,
//        RequireClientSecret = false,
//        RedirectUris = { "dk.duende.xamarin" },
//        RequireConsent = false,
//        RequirePkce = true,
//        PostLogoutRedirectUris = { $"dk.duende.xamarin/Account/Redirecting" },
//        AllowedScopes = new List<string>
//        {
//            IdentityServerConstants.StandardScopes.OpenId,
//            IdentityServerConstants.StandardScopes.Profile,
//            IdentityServerConstants.StandardScopes.OfflineAccess,
//            "Api1"
//        },
//        AllowAccessTokensViaBrowser = true
//    }
//};
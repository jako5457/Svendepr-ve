﻿using Duende.IdentityServer;
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
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "Mailverf",
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.EmailVerified,
                        JwtClaimTypes.Role
                    }
                }
                
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("company_read", "read company data"),
                new ApiScope("company_write", "Edit/create company data"),

                new ApiScope("driver_read", "Read driver information"),
                new ApiScope("driver_write", "Edit/create driver information"),

                new ApiScope("employee_read", "Read employees"),
                new ApiScope("employee_write", "edit/create employees"),

                new ApiScope("order_read", "Read order information"),
                new ApiScope("order_write", "Edit/create order information"),

                new ApiScope("product_read", "Read product information"),
                new ApiScope("product_write", "Edit/create product"),

                new ApiScope("product_request_read", "Read order requests"),
                new ApiScope("product_request_write", "Edit/create order requests"),

                new ApiScope("warehouse_read", "Read warehouse information"),
                new ApiScope("warehouse_write", "Edit/create warehouse information"),
            };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
            },
            new Client
            {
                ClientId = "web",
                ClientSecrets = {new Secret("websecret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,

                //RequirePkce = false,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "company_read",
                    "company_write",
                    "driver_read",
                    "driver_write",
                    "employee_read",
                    "employee_write",
                    "order_read",
                    "order_write",
                    "product_read",
                    "product_write",
                    "product_request_read",
                    "product_request_write",
                    "warehouse_read",
                    "warehouse_write",
                    "api_all_read",
                    "api_all_write",
                    "Mailverf"
                }

            },
            new Client
            {
                ClientId = "Api",
                ClientSecrets = {new Secret("Apisecret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                // where to redirect to after login
                RedirectUris = { "https://localhost:7039/swagger/oauth2-redirect.html" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "company_read",
                    "company_write",
                    "driver_read",
                    "driver_write",
                    "employee_read",
                    "employee_write",
                    "order_read",
                    "order_write",
                    "product_read",
                    "product_write",
                    "product_request_read",
                    "product_request_write",
                    "warehouse_read",
                    "warehouse_write",
                    "api_all_read",
                    "api_all_write",
                }

            },
            new Client
            {
                ClientId = "Api2",
                ClientSecrets = {new Secret("Apisecret2".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                // where to redirect to after login
                RedirectUris = { "https://svendproveapi.azurewebsites.net/swagger/oauth2-redirect.html" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "company_read",
                    "company_write",
                    "driver_read",
                    "driver_write",
                    "employee_read",
                    "employee_write",
                    "order_read",
                    "order_write",
                    "product_read",
                    "product_write",
                    "product_request_read",
                    "product_request_write",
                    "warehouse_read",
                    "warehouse_write",
                    "api_all_read",
                    "api_all_write",
                }

            },
            new Client
            {
                ClientId = "native",
                RequireClientSecret = false,
                //ClientSecrets = { new Secret("nativesecret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowOfflineAccess = true,

                RedirectUris = {"com.companyname.xamarinclient://callback"},

                PostLogoutRedirectUris = { "com.companyname.xamarinclient://callback" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "company_read",
                    "company_write",
                    "driver_read",
                    "driver_write",
                    "employee_read",
                    "employee_write",
                    "order_read",
                    "order_write",
                    "product_read",
                    "product_write",
                    "product_request_read",
                    "product_request_write",
                    "warehouse_read",
                    "warehouse_write"
                }
            }
        };
    }
}
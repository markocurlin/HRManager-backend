using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace HRManager.STS
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("projects-api", "Projects API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("projects-api", "Projects API")
                {
                    Scopes = { "projects-api" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "spa-client",
                    ClientName = "Projects SPA",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                      
                    RedirectUris =           { "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4200/signout-callback" },
                    AllowedCorsOrigins =     { "http://localhost:4200" },
                    /*

                   
                    RedirectUris =           { "https://192.168.1.21:443/signin-callback", "https://192.168.1.21:443/assets/silent-callback.html" },
                    PostLogoutRedirectUris = { "https://192.168.1.21:443/signout-callback" },
                    AllowedCorsOrigins =     { "https://192.168.1.21:443" },
*/
/*
                    RedirectUris =           { "http://localhost:443/signin-callback", "http://localhost:443/assets/silent-callback.html", "https://localhost/signin-callback", "https://localhost/assets/silent-callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:443/signout-callback", "https://localhost/signout-callback" },
                    AllowedCorsOrigins =     { "http://localhost:443", "https://localhost" },
*/
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "projects-api"
                    },
                    AccessTokenLifetime = 600
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "https://localhost:4201/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:4201/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowOfflineAccess = true

                }
            };
    }
}
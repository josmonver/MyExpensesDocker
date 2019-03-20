using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyExpenses.IdentityServerHost
{
    public static class Config
    { 
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("ExpensesApi", "Expenses API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            //return new List<Client>();

            return new List<Client>
            {
                new Client
                {
                    ClientId = "TourOfHeroes",
                    ClientName = "TourOfHeroes Client",
                    ClientUri = "http://localhost:4200",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "http://localhost:4200/auth-callback", "http://localhost:4200/silent-refresh.html" }, // client app uri
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins =     { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ExpensesApi"
                    }
                }
            };
        }
    }
}

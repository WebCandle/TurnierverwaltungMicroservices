using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public static class Config
    {
        public static List<ApiScope> GetScopes()
        {
            return new List<ApiScope>
                {
                    new ApiScope
                        {
                            Name = "apiscope"
                        }
                };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("apiresource")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
            new Client
            {
                ClientId = "user",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("user".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "apiscope" }
            }
        };
        }
    }
}

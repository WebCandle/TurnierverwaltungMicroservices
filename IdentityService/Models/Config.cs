using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("myresourceapi", "My Resource API")
                {
                    Scopes = {new Scope("apiscope")}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // for public api
                new Client
                {
                    ClientId = "user",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("user".Sha256())
                    },
                    AllowedScopes = { "apiscope" },
                    Claims = new List<Claim> { new Claim(JwtClaimTypes.Role, "user")}
                },
                new Client
                {
                    ClientId = "admin",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("admin".Sha256())
                    },
                    AllowedScopes = { "apiscope" },
                    Claims = new List<Claim> { new Claim(JwtClaimTypes.Role, "user"), new Claim(JwtClaimTypes.Role, "admin") }
                }
            };
        }
    }
}

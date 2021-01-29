using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService.Models
{
    internal class Users
    {
        //public static List<Client> Clients { get; set; }
        public static IEnumerable<Client> GetClients()
        {
            var Clients = new List<Client>();
            Clients.Add(new Client
            {
                ClientId = "user",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                    {
                        new Secret("user".Sha256())
                    },
                AllowedScopes = { "MannschaftApiScope.read","PersonApiScope.read" }
            });
            Clients.Add(new Client
            {
                ClientId = "admin",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                    {
                        new Secret("admin".Sha256())
                    },
                AllowedScopes = { "MannschaftApiScope.write", "PersonApiScope.write", "MannschaftApiScope.read", "PersonApiScope.read" }
            });
            Clients.Add(new Client
            {
                ClientId = "oidcClient",
                ClientName = "Example Client Application",
                ClientSecrets = new List<Secret> { new Secret("oidcClient".Sha256()) }, // change me!

                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string> { "http://localhost:5004/signin-oidc" },
                AllowedScopes = new List<string>
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,
        "role",
        "MannschaftApiScope.write", "PersonApiScope.write", "MannschaftApiScope.read", "PersonApiScope.read"
    },

                RequirePkce = true,
                AllowPlainTextPkce = false
            });
            return Clients;
        }
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
            new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "maher",
                Password = "maher",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            }
        };
        }
    }
}

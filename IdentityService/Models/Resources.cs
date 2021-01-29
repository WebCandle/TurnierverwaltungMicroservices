using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService.Models
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
            new ApiResource
            {
                Name = "ApiResource",
                DisplayName = "ApiResource",
                Description = "Allow the application to access API #1 on your behalf",
                Scopes = new List<string> {"MannschaftApiScope.write", "PersonApiScope.write", "MannschaftApiScope.read", "PersonApiScope.read"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            new ApiScope("MannschaftApiScope.read", "Read Access to API-Mannschaft"),
            new ApiScope("MannschaftApiScope.write", "Write Access to API-Mannschaft"),
            new ApiScope("PersonApiScope.read", "Read Access to API-Person"),
            new ApiScope("PersonApiScope.write", "Write Access to API-Person")
        };
        }
    }
}

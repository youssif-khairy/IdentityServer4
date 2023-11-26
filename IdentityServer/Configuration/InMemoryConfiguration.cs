using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer.Configuration
{
    public static class InMemoryConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()
          };

        public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4", // from OpenId Resource
                  Username = "admin",
                  Password = "Xyz78900",
                  Claims = new List<Claim>
                  {
                      /* all profile claims [name , family_name , given_name , middle_name , nickname , preferred_username , profile , picture , website , gender , birthdate , zoneinfo , locale , updated_at ]*/
                      new Claim("given_name", "admin"), // from  Profile Resource
                      new Claim("family_name", "admin_family")// from  Profile Resource
                  }
              },
              new TestUser
              {
                  SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                  Username = "youssif",
                  Password = "Xyz78900",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "youssef"),
                      new Claim("family_name", "khairy")
                  }
              }
          };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "client_identity",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials, // both grantTypes
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId , "IdentityServer4.API.Scope" }
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { 
                new ApiScope("IdentityServer4.API.Scope", "IdentityServer API Scope")
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("IdentityServer4.API.Resource", "IdentityServer API Resource")
                {
                    Scopes = { "IdentityServer4.API.Scope" }
                }
            };
    }
}

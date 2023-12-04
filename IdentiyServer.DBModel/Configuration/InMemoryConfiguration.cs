using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer.DBModel.Configuration
{
    public static class InMemoryConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResource("roles",new List<string> { "role" } ),
          };

        public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b5", // from OpenId Resource
                  Username = "admin",
                  Password = "Xyz78900",
                  Claims = new List<Claim>
                  {
                      /* all profile claims [name , family_name , given_name , middle_name , nickname , preferred_username , profile , picture , website , gender , birthdate , zoneinfo , locale , updated_at ]*/
                      new Claim("given_name", "admin"), // from  Profile Resource
                      new Claim("family_name", "admin_family"),// from  Profile Resource
                      new Claim("address", "cairo address"),// from  address resource
                      new Claim("role", "Admin"),
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
                      new Claim("family_name", "khairy"),
                      new Claim("role", "visitor"),
                  }
              },
          };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "client_identity",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials, // both grantTypes
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId , IdentityServerConstants.StandardScopes.Address ,"IdentityServer4.API.Scope", "roles" }
                },
               new Client
                {
                    ClientName = "Angular-Client",
                    ClientId = "angular-client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>{ "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address ,
                       "IdentityServer4.API.Scope",
                       "roles"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
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

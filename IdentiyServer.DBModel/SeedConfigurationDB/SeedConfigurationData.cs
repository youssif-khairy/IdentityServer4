using IdentityServer.DBModel.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace IdentiyServer.DBModel.SeedConfigurationDB
{
    public static class SeedConfigurationData
    {
        public static IServiceProvider MigrateIdentiyDatabase(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                try
                {
                    context.Database.Migrate();
                    if (!context.Clients.Any())
                    {
                        foreach (var client in InMemoryConfiguration.GetClients())
                        {
                            context.Clients.Add(client.ToEntity());
                        }
                        context.SaveChanges();
                    }
                    if (!context.IdentityResources.Any())
                    {
                        foreach (var resource in InMemoryConfiguration.GetIdentityResources())
                        {
                            context.IdentityResources.Add(resource.ToEntity());
                        }
                        context.SaveChanges();
                    }
                    if (!context.ApiScopes.Any())
                    {
                        foreach (var apiScope in InMemoryConfiguration.GetApiScopes())
                        {
                            context.ApiScopes.Add(apiScope.ToEntity());
                        }
                        context.SaveChanges();
                    }
                    if (!context.ApiResources.Any())
                    {
                        foreach (var resource in InMemoryConfiguration.GetApiResources())
                        {
                            context.ApiResources.Add(resource.ToEntity());
                        }
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
                return services;
            }
            
        }
    }
}

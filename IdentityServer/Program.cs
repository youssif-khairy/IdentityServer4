using IdentityServer.DBModel.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using IdentiyServer.DBModel.SeedConfigurationDB;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.



builder.Services.AddControllers();



builder.Services.AddIdentityServer()
        .AddTestUsers(InMemoryConfiguration.GetUsers())
        //.AddInMemoryApiScopes(InMemoryConfiguration.GetApiScopes())
        //.AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
        //.AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
        //.AddInMemoryClients(InMemoryConfiguration.GetClients())
        .AddConfigurationStore(opt =>
        {
            opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("IdentityConfigurationDB"),
                sql => sql.MigrationsAssembly("IdentiyServer.DBModel"));
        })
        .AddOperationalStore(opt =>
        {
            opt.ConfigureDbContext = o => o.UseSqlServer(Configuration.GetConnectionString("IdentityConfigurationDB"),
                sql => sql.MigrationsAssembly("IdentiyServer.DBModel"));
        })
        .AddDeveloperSigningCredential(); //to presists tempkey.jwk file as this file is containing key for validating the generated jwt tokens ( for development purpose only ) ,in production you should generate certificate

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Services.MigrateIdentiyDatabase();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();


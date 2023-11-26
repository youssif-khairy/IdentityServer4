using IdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddIdentityServer()
        .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
        .AddTestUsers(InMemoryConfiguration.GetUsers())
        .AddInMemoryClients(InMemoryConfiguration.GetClients())
        .AddDeveloperSigningCredential(); //to presists tempkey.jwk file as this file is containing key for validating the generated jwt tokens ( for development purpose only ) ,in production you should generate certificate


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();


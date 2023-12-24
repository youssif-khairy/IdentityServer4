using IdentityServer.DBModel.Configuration;
using IdentiyServer.DBModel.Configuration;
using IdentiyServer.DBModel.SeedConfigurationDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddIdentityServer(opt =>
{
    opt.Authentication.CookieLifetime = TimeSpan.FromMinutes(60);//  minutes for idp cookie that open sessoin for lgged in user
    opt.Authentication.CookieSameSiteMode = SameSiteMode.Lax; // to work with http 
})
.AddTestUsers(InMemoryConfiguration.GetUsers())
//.AddInMemoryApiScopes(InMemoryConfiguration.GetApiScopes())
//.AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
//.AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
//.AddInMemoryClients(InMemoryConfiguration.GetClients())
.AddProfileService<CustomProfileService>() //to add user claims to access token
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
var allowedOrigins = Configuration.GetSection("AllowedCors").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseCors(x =>
                x.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

app.Services.MigrateIdentiyDatabase();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer(); 

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapRazorPages();

app.Run();

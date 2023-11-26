using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IdentityServer4.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuthenticationJWT(this IServiceCollection services, ConfigurationManager  configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = configuration.GetSection("IdentityServerURL").Value;
                options.Audience = configuration.GetSection("IdentityServerResource").Value;
            });

            return services;
        }
    }
}

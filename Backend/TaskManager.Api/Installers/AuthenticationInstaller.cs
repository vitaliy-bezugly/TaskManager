using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Common;
using TaskManager.Api.Installers.Abstract;

namespace TaskManager.Api.Installers;

public class AuthenticationInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("Authentication");
        services.Configure<AuthenticationOptions>(options);

        // Configure authentication (based on jwt tokens)
        AuthenticationOptions? authOptions = options.Get<AuthenticationOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddHttpContextAccessor();
    }
}

using TaskManager.Api.Persistence;
using TaskManager.Api.Installers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
    {
        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        if(connectionString == null)
            logger.LogInformation("There is no connection string as environment variable. " +
                    "App will use sqlite database");
        else 
            logger.LogInformation("Connection string has been found as environment variable. " +
                    "App will use sql server");

        services.AddDbContext<ApplicationDataContext>(options =>
        {
            if(connectionString == null)
            {
                options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
            }
            else
            {
                options.UseSqlServer(connectionString);
            }
        });
    }
}

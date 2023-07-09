using TaskManager.Api.Persistence;
using TaskManager.Api.Installers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Installers;

public class DatabaseInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
    {
        var connectionString = GetConnectionStringOrNull(configuration);
        if (String.IsNullOrEmpty(connectionString))
        {
            logger.LogError("There is no connection string has been found. The application will use InMemoryDatabase.");
            services.AddDbContext<ApplicationDataContext>(options => options.UseInMemoryDatabase("TaskManager"));
        }

        services.AddDbContext<ApplicationDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static string? GetConnectionStringOrNull(IConfiguration configuration)
    {
        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        if (String.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        return connectionString;
    }
}

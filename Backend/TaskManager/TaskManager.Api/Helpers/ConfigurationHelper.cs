using Microsoft.EntityFrameworkCore;
using Persistence;

namespace TaskManager.Helpers;

public static class ConfigurationHelper
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = null;
        // If it isn't run in a docker
        if (configuration["IsDocker"] == null)
        {
            connectionString = configuration.GetConnectionString("ConnectionToDatabase");
        }
        else
        {
            // Test values
            string server = configuration["DbServer"] ?? "ms-sql-server";
            string port = configuration["Port"] ?? "1433";
            string user = configuration["Dbuser"] ?? "SA";
            string password = configuration["DbPassword"] ?? "BilliJin2000";
            string database = configuration["Database"] ?? "Bookstore";
    
            connectionString = $"Data Source={server},{port};Persist Security Info=True;" +
                               $"Initial Catalog={database};User ID={user};Password={password}";
        }
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}
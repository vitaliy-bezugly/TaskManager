using Domain.Services;
using Domain.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using Persistence.Repositories.Abstract;
using Serilog;
using Services;

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
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAccountService, AccountService>();
    }
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.AddSerilog(logger);
    }
}
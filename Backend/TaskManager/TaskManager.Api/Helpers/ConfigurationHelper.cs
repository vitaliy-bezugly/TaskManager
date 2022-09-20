using Authentication.Common;
using Domain.Services;
using Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using Persistence.Repositories.Abstract;
using Serilog;
using Services;
using Persistence.Repositories.Cached;
using Microsoft.IdentityModel.Tokens;

namespace TaskManager.Helpers;

public static class ConfigurationHelper
{
    public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = "";
       
        // If it isn't run in a docker
        if (configuration["IsDocker"] == null)
        {
            connectionString = configuration.GetConnectionString("ConnectionToDatabase");
        }
        else
        {
            string server = configuration["DbServer"] ?? "ms-sql-server";
            string port = configuration["DbPort"] ?? "1433";
            string user = configuration["DbUser"] ?? "SA";
            string password = configuration["DbPassword"] ?? "BilliJin2000";
            string database = configuration["DbCatalog"] ?? "Bookstore";
    
            connectionString = $"Data Source={server},{port};Persist Security Info=True;" +
                               $"Initial Catalog={database};User ID={user};Password={password}";
        }
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
    public static void ConfigureRedisConnection(this IServiceCollection services, IConfiguration configuration)
    {
        string redisConfiguration = "", redisInstanceName = "";

        // It isn't run in a docker
        if (configuration["IsDocker"] == null)
        {
            var redisSection = configuration.GetSection("RedisConnection");

            redisConfiguration = redisSection.GetValue<string>("Url");
            redisInstanceName = redisSection.GetValue<string>("Instance");
        }
        else
        {
            services.AddStackExchangeRedisCache(options =>
            {
                redisConfiguration = configuration["RedisUrl"] ?? "redis";
                redisInstanceName = configuration["RedisInstanceName"] ?? "RedisCache";
            });
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfiguration;
            options.InstanceName = redisInstanceName;
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

        services.Decorate<ITaskRepository, TaskRepositoryCached>();
    }
    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.AddSerilog(logger);
    }
    public static void ConfigureAuthentication(this IServiceCollection services, AuthenticationOptions authOptions)
    {
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
    }
}
using TaskManager.Refactored.Installers.Abstract;
using TaskManager.Refactored.Services;
using TaskManager.Refactored.Services.Abstract;
using TaskManager.Refactored.Services.Strategy;
using TaskManager.Refactored.Services.Strategy.Abstract;

namespace TaskManager.Refactored.Installers;

public class ServicesInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<IGeneratorGwtStrategy, DefaultGeneratorGwtStrategy>();
        services.AddSingleton<IClaimParser, ClaimParser>();
    }
}

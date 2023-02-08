using TaskManager.Api.Installers.Abstract;
using TaskManager.Api.Services;
using TaskManager.Api.Services.Abstract;
using TaskManager.Api.Services.Strategy;
using TaskManager.Api.Services.Strategy.Abstract;

namespace TaskManager.Api.Installers;

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

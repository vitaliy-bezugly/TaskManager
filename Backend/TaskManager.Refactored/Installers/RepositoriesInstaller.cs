using TaskManager.Api.Installers.Abstract;
using TaskManager.Api.Repositories;
using TaskManager.Api.Repositories.Abstract;

namespace TaskManager.Api.Installers;

public class RepositoriesInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}

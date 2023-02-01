using TaskManager.Refactored.Installers.Abstract;
using TaskManager.Refactored.Repositories;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Installers;

public class RepositoriesInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}

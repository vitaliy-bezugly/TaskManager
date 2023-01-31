using TaskManager.Refactored.Installers.Abstract;
using TaskManager.Refactored.Repositories;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Installers;

public class TaskRepositoryInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}

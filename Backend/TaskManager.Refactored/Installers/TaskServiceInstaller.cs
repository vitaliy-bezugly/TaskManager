using TaskManager.Refactored.Installers.Abstract;
using TaskManager.Refactored.Services;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Installers;

public class TaskServiceInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskService, TaskService>();
    }
}

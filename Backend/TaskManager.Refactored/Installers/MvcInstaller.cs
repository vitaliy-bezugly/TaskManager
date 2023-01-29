using TaskManager.Refactored.Installers.Abstract;

namespace TaskManager.Refactored.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }
}

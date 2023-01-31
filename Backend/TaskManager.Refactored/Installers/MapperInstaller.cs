using TaskManager.Refactored.Installers.Abstract;

namespace TaskManager.Refactored.Installers;

public class MapperInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup));
    }
}

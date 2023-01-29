using TaskManager.Refactored.Installers.Abstract;

namespace TaskManager.Refactored.Installers;

public class SwaggerInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();
    }
}

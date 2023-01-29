namespace TaskManager.Refactored.Installers.Abstract;

public interface IInstaller
{
    void InstallService(IServiceCollection services, IConfiguration configuration);
}

namespace TaskManager.Api.Installers.Abstract;

public interface IInstaller
{
    void InstallService(IServiceCollection services, IConfiguration configuration);
}

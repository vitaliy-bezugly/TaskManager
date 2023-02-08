using TaskManager.Api.Installers.Abstract;

namespace TaskManager.Api.Installers.Extensions;

public static class InstallerExtensions
{
    public static void InstallServiceInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = typeof(Startup).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        installers.ForEach(installer => installer.InstallService(services, configuration));
    }
}
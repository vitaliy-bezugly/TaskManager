using TaskManager.Api.Persistence;
using TaskManager.Api.Installers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDataContext>(options =>
            options.UseInMemoryDatabase("Inmemorydb"));
    }
}

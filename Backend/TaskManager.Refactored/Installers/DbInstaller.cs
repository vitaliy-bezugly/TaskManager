using TaskManager.Refactored.Persistence;
using TaskManager.Refactored.Installers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Refactored.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));
    }
}

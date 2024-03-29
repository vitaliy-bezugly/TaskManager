﻿using TaskManager.Api.Installers.Abstract;

namespace TaskManager.Api.Installers;

public class MapperInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration,
        ILogger<Startup> logger)
    {
        services.AddAutoMapper(typeof(Startup));
    }
}

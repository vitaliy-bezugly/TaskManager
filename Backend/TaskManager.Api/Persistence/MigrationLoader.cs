using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Persistence;

public static class MigrationLoader
{
    public static void Load(IApplicationBuilder applicationBuilder, ILogger logger)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDataContext>();

        if(context == null)
        {
            logger.LogError("Context is null. Can not apply migration");
            return;
        }

        ApplyMigration(context, logger);
    }

    private static void ApplyMigration(ApplicationDataContext context, ILogger logger)
    {
        logger.LogInformation("Applying migration ...");
        context.Database.Migrate();
    }
}
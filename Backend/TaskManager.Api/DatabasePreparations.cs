using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Persistence;

namespace TaskManager.Api;

public class DatabasePreparations
{
    public static void Prepare(IApplicationBuilder applicationBuilder, ILogger logger)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDataContext>();

            if(context == null)
            {
                logger.LogError("Context is null. Can not apply migration");
                return;
            }

            ApplyMigration(context, logger);
        }
    }

    private static void ApplyMigration(ApplicationDataContext context, ILogger logger)
    {
        logger.LogInformation("Applying migration ...");
        context.Database.Migrate();
    }
}

using TaskManager.Api.Installers.Extensions;

namespace TaskManager.Api;

public class Startup
{
    public IConfiguration Configuration
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServiceInAssembly(Configuration);
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == 404 && System.IO.Path.HasExtension(context.Request.Path.Value) == false)
            {
                context.Request.Path = "/index.html";
                await next();
            }
        });

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors();

        app.MapControllers();

        app.Run();
    }
}

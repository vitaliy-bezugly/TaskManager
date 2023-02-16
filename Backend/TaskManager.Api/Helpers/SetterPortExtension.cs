namespace TaskManager.Api.Helpers;

public static class SetterPortExtension
{
    public static void ConfigurePortToListen(this WebApplicationBuilder builder)
    {
        string? port = Environment.GetEnvironmentVariable("PORT");
        if (port != null)
        {
            builder.WebHost.UseUrls($"http://*:{port}");
        }
    }
}

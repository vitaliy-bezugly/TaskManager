using Microsoft.Extensions.Logging.Console;
using TaskManager.Api;

var builder = WebApplication.CreateBuilder(args);
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Enabled);
});
var logger = loggerFactory.CreateLogger<Startup>();

var startup = new Startup(builder.Configuration, logger);
startup.ConfigureServices(builder.Services);            // calling ConfigureServices method

var app = builder.Build();
startup.Configure(app, builder.Environment);            // calling Configure method
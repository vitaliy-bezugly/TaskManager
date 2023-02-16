using TaskManager.Api;
using TaskManager.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);            // calling ConfigureServices method
builder.ConfigurePortToListen();

var app = builder.Build();

startup.Configure(app, builder.Environment);            // calling Configure method
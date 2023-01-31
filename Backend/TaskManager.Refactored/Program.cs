using TaskManager.Refactored;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method

var app = builder.Build();

startup.Configure(app, builder.Environment); // calling Configure method

/*
 * {
  "title": "Test adding",
  "description": "Test description",
  "isImportant": true,
  "expirationTime": "2023-02-11T09:36:35.294Z"
}
 */
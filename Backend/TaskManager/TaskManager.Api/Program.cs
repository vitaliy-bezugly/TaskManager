using Authentication.Common;
using TaskManager.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();

var options = builder.Configuration.GetSection("Authentication");
builder.Services.Configure<AuthenticationOptions>(options);

var app = builder.Build();

DatabasePreparation.PreparePopulation(app, app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
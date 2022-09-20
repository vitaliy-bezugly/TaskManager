using Authentication.Common;
using TaskManager.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureLogger();

builder.Services.ConfigureDatabaseConnection(builder.Configuration);

builder.Services.ConfigureRedisConnection(builder.Configuration);
builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();

var options = builder.Configuration.GetSection("Authentication");
builder.Services.Configure<AuthenticationOptions>(options);

// Configure authentication (based on jwt tokens)
AuthenticationOptions? authOptions = options.Get<AuthenticationOptions>();
builder.Services.ConfigureAuthentication(authOptions);
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

DatabasePreparation.PreparePopulation(app, app.Logger);

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

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
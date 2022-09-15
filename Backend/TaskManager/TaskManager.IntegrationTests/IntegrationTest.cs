using Authentication.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TaskManager.ViewModels;

namespace TaskManager.IntegrationTests;

public class IntegrationTest
{
    protected HttpClient _httpClient;
    protected const string _apiUrl = "https://localhost:7183/api/";

    [OneTimeSetUp]
    public void Setup()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                });
            });
        _httpClient = webApplicationFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        string jwt = await GetJwtAsync();
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", jwt);
    }

    private async Task<HttpResponseMessage> RegisterUserAsync()
    {
        var registerForm = new RegisterViewModel
        {
            Email = "testEmail@gmail.com",
            Password = "strongPass123",
            Username = "testUser",
            Roles = new List<string> { "user" }
        };

        string json = JsonSerializer.Serialize<RegisterViewModel>(registerForm);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        return await _httpClient.PostAsync(_apiUrl + "Account/Register",
            payload);
    }

    private async Task<string> GetJwtAsync()
    {
        HttpResponseMessage response = await RegisterUserAsync();

        var authResponseJson = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthenticationSuccessResponse>(authResponseJson);
        return authResponse.access_token;
    }
}

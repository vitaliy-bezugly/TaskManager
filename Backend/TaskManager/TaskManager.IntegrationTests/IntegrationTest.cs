using Authentication.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Net;
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
    [TearDown]
    public void TearDown()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
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

    protected async Task<TaskViewModel?> GetTaskAsync(string taskId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl + "Task" + $"/{taskId}");
        HttpStatusCode statusCode = response.StatusCode;

        if (statusCode != HttpStatusCode.OK)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            throw new Exception(errorMessage);
        }

        return await JsonSerializer.DeserializeAsync<TaskViewModel>
            (await response.Content.ReadAsStreamAsync());
    }
    protected async Task<IEnumerable<TaskViewModel>?> GetTasksAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl + "Task");
        HttpStatusCode statusCode = response.StatusCode;

        if (statusCode != HttpStatusCode.OK)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            throw new Exception(errorMessage);
        }

        return await JsonSerializer
            .DeserializeAsync<List<TaskViewModel>>(await response.Content.ReadAsStreamAsync());
    }
    protected async Task<HttpResponseMessage> CreateTaskAsync(TaskViewModel task)
    {
        string jsonTask = JsonSerializer.Serialize<TaskViewModel>(task);
        var payload = new StringContent(jsonTask, Encoding.UTF8, "application/json");

        return await _httpClient.PostAsync(_apiUrl + "Task", payload);
    }
    protected async Task<List<HttpResponseMessage>> CreateTasksAsync(List<TaskViewModel> tasks)
    {
        var result = new List<HttpResponseMessage>();

        foreach (var task in tasks)
        {
            string jsonTask = JsonSerializer.Serialize<TaskViewModel>(task);
            var payload = new StringContent(jsonTask, Encoding.UTF8, "application/json");

            result.Add(await _httpClient.PostAsync(_apiUrl + "Task", payload));
        }

        return result;
    }
    protected async Task<HttpResponseMessage> UpdateTask(string taskId, TaskViewModel task)
    {
        string jsonTask = JsonSerializer.Serialize<TaskViewModel>(task);
        var payload = new StringContent(jsonTask, Encoding.UTF8, "application/json");

        return await _httpClient.PutAsync(_apiUrl + "Task" + $"/{taskId}", payload);
    }
    protected async Task<HttpResponseMessage> DeleteTask(string taskId)
    {
        return await _httpClient.DeleteAsync(_apiUrl + "Task" + $"/{taskId}");
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TaskManager.Api;
using TaskManager.Api.Contracts.v1;
using TaskManager.Api.Contracts.v1.Requests;
using TaskManager.Api.Contracts.v1.Responses;
using TaskManager.Api.Persistence;
using TaskManager.IntegrationTests.Environment;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.IntegrationTests;

public class IntegrationTests
{
    protected readonly HttpClient httpClient;
    private static string _accessToken = null;
    public IntegrationTests()
    {
        var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDataContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<ApplicationDataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });
        httpClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        if(_accessToken == null)
        {
            _accessToken = await GetJwtAsync();
        }

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", 
            _accessToken);
    }
    private async Task<string> GetJwtAsync()
    {
        var response = await httpClient.PostAsJsonAsync(ApiRoutes.Account.Register, new AccountRegistrationRequest
        {
            Username = AccountData.Username,
            Email = AccountData.Email,
            Password = AccountData.Password
        });

        var json = await response.Content.ReadAsStringAsync();
        var responseToken = JsonSerializer.Deserialize<AuthorizationSuccessResponse>(json);
        return responseToken.access_token;
    }

    protected async Task<HttpResponseMessage> CreateTaskAsync(CreateTaskRequest taskRequest)
    {
        return await httpClient.PostAsJsonAsync(ApiRoutes.Task.Create, taskRequest);
    }
    protected async Task<HttpResponseMessage> UpdateTaskAsync(Guid taskId, UpdateTaskRequest taskRequest)
    {
        return await httpClient.PutAsJsonAsync(ApiRoutes.Task.Update.Replace("{taskId}", taskId.ToString())
            , taskRequest);
    }
    protected async Task<HttpResponseMessage> DeleteTaskAsync(Guid taskId)
    {
        return await httpClient.DeleteAsync(ApiRoutes.Task.Delete.Replace("{taskId}", taskId.ToString()));
    }
}

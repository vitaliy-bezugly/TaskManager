using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TaskManager.Api;
using TaskManager.Api.Contracts.v1;
using TaskManager.Api.Contracts.v1.Requests;
using TaskManager.Api.Contracts.v1.Responses;

namespace TaskManager.IntegrationTests;

public class IntegrationTests
{
    protected readonly HttpClient httpClient;
    public IntegrationTests()
    {
        var appFactory = new WebApplicationFactory<Startup>();
        httpClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", 
            await GetJwtAsync());
    }

    private async Task<string> GetJwtAsync()
    {
        var response = await httpClient.PostAsJsonAsync(ApiRoutes.Account.Login, new AccountLoginRequest
        {
            Email = "test@integration.com",
            Password = "String123!"
        });

        var json = await response.Content.ReadAsStringAsync();
        var responseToken = JsonSerializer.Deserialize<AuthorizationSuccessResponse>(json);
        return responseToken.access_token;
    }

    [Fact]
    public async Task GetAll_WithoutAnyPosts_ReturnsEmptyCollection()
    {
        // Arrange
        await AuthenticateAsync();

        // Act
        var response = await httpClient.GetAsync(ApiRoutes.Task.GetAll);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<List<GetTaskResponse>>(content).Should().BeEmpty();
    }
}

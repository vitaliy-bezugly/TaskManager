using FluentAssertions;
using System.Net;
using System.Text.Json;
using TaskManager.Api.Contracts.v1;
using TaskManager.Api.Contracts.v1.Requests;
using TaskManager.Api.Contracts.v1.Responses;
using TaskManager.IntegrationTests.Extensions;

namespace TaskManager.IntegrationTests;

public class TaskTests : IntegrationTests
{
    [Fact]
    public async Task GetAll_WithoutAnyPosts_ReturnsOkAndTasksAreNotNull()
    {
        // Arrange
        await AuthenticateAsync();

        // Act
        var response = await httpClient.GetAsync(ApiRoutes.Task.GetAll);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadAsJsonAsync<List<GetTaskResponse>>()).Should().NotBeNull();
    }
    [Fact]
    public async Task Get_TaskExistsInDatabase_ReturnsTask()
    {
        // Arrange
        await AuthenticateAsync();
        var responseFromCreation = await CreateTaskAsync(new CreateTaskRequest
        {
            Title = "To test",
            Description = null, 
            IsImportant = false,
            ExpirationTime = DateTime.Now.AddDays(3)
        });

        // Act
        Guid createdTaskId = (await responseFromCreation.Content.ReadAsJsonAsync<CreateTaskResponse>()).id; 
        var responseFromGetRequest = await httpClient.GetAsync(ApiRoutes.Task.Get.Replace("{taskId}", 
            createdTaskId.ToString()));

        // Assert
        responseFromGetRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        (await responseFromGetRequest.Content.ReadAsJsonAsync<GetTaskResponse>()).Should().NotBeNull();
    }
    [Fact]
    public async Task Create_TaskWithValidData_ReturnsCreatedCode()
    {
        // Arrange
        await AuthenticateAsync();

        // Act
        var taskToCreate = new CreateTaskRequest
        {
            Title = "Creation test",
            Description = null,
            IsImportant = true,
            ExpirationTime = DateTime.Now.AddDays(3)
        };
        var response = await CreateTaskAsync(taskToCreate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        (await response.Content.ReadAsJsonAsync<CreateTaskResponse>()).title.Should().BeEquivalentTo(taskToCreate.Title);
    }
    [Fact]
    public async Task Update_TaskExistsInDatabase_ReturnsUpdatedCode()
    {
        // Arrange
        await AuthenticateAsync();

        var taskToCreate = new CreateTaskRequest
        {
            Title = "New task",
            Description = null,
            IsImportant = false,
            ExpirationTime = DateTime.Now.AddDays(3)
        };
        var responseFromCreate = await CreateTaskAsync(taskToCreate);

        // Act
        var taskToUpdate = new UpdateTaskRequest
        {
            Title = "Updated title",
            Description = "Updated description",
            IsImportant = taskToCreate.IsImportant,
            ExpirationTime = taskToCreate.ExpirationTime
        };
        var response = await UpdateTaskAsync((await responseFromCreate
            .Content.ReadAsJsonAsync<CreateTaskResponse>()).id, taskToUpdate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadAsJsonAsync<UpdateTaskResponse>()).title.Should().BeEquivalentTo(taskToUpdate.Title);
    }
    [Fact]
    public async Task Delete_TaskExistsInDatabase_ReturnsNoContent()
    {
        // Arrange
        await AuthenticateAsync();

        var taskToCreate = new CreateTaskRequest
        {
            Title = "Task to delete in future",
            Description = "Test delete operation",
            IsImportant = true,
            ExpirationTime = DateTime.Now.AddDays(3)
        };
        var responseFromCreate = await CreateTaskAsync(taskToCreate);

        // Act
        Guid id = (await responseFromCreate.Content.ReadAsJsonAsync<CreateTaskResponse>()).id;
        var response = await DeleteTaskAsync(id);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
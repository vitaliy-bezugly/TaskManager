using System.Net;
using System.Text.Json;
using TaskManager.ViewModels;

namespace TaskManager.IntegrationTests.ControllerTests;

public class TaskControllerTests : TaskTest
{
    [Test]
    public async Task GetTasks_GetTasksWithoutBearer_Unauthorize()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl + "Task");
        HttpStatusCode actual = response.StatusCode;

        var expected = HttpStatusCode.Unauthorized;

        Assert.That(actual, Is.EqualTo(expected));
    }
    [Test]
    public async Task GetTasks_WithoutCreation_EmptyResponse()
    {
        await AuthenticateAsync();

        var actualTasks = await GetTasksAsync();

        var expectedTasks = new List<TaskViewModel> { };

        CollectionAssert.AreEqual(expectedTasks, actualTasks);
    }
    [Test]
    public async Task CreateTasks_CreateMultiplyTasks_ListOfTasks()
    {
        await AuthenticateAsync();

        var tasks = new List<TaskViewModel>();
        for (int i = 1; i <= 3; i++)
        {
            tasks.Add(new TaskViewModel
            {
                title = $"test task {i}",
                description = $"test description {i}",
                expirationTime = DateTime.Now.AddDays(i)
            });
        }

        List<HttpResponseMessage> responses = await CreateTasksAsync(tasks);
        foreach (HttpResponseMessage response in responses)
            CheckStatusCodeIfNotValidFailTest(response);

        List<TaskViewModel> actualTasks = (await GetTasksAsync()).ToList();
        List<TaskViewModel> expectedTasks = tasks;

        foreach (var item in expectedTasks)
        {
            if (actualTasks.Exists(x => x.title == item.title && x.description == item.description &&
                item.expirationTime == x.expirationTime) == false)
            {
                Assert.Fail($"There are no item from server with title: {item.title} and desc:{item.description}");
            }
        }

        Assert.Pass();
    }
    [Test]
    public async Task CreateTask_TaskViewModel_OkResponse()
    {
        await AuthenticateAsync();
        var task = new TaskViewModel
        {
            title = "testing task creation",
            description = "test description",
            expirationTime = DateTime.Now.AddDays(2)
        };

        HttpResponseMessage response = await CreateTaskAsync(task);
        CheckStatusCodeIfNotValidFailTest(response);

        Assert.Pass();
    }
    [Test]
    public async Task UpdateTask_UpdateOperation_OkResponse()
    {
        await AuthenticateAsync();

        var task = new TaskViewModel
        {
            title = $"testing tast updation",
            description = $"test description",
            expirationTime = DateTime.Now.AddDays(3)
        };

        HttpResponseMessage response = await CreateTaskAsync(task);
        CheckStatusCodeIfNotValidFailTest(response);

        IEnumerable<TaskViewModel> tasks = await GetTasksAsync();
        TaskViewModel? taskToUpdate = tasks.FirstOrDefault(x => x.title == task.title &&
            x.description == task.description);

        if (taskToUpdate == null)
            Assert.Fail("Task has not been created");

        task.description = "updated";

        HttpResponseMessage updateResponse = await UpdateTask(taskToUpdate.id, task);
        CheckStatusCodeIfNotValidFailTest(updateResponse);

        Assert.Pass("Task was successfully updated");
    }
    [Test]
    public async Task UpdateTask_UpdateMehodGetUpdatedTask_UpdatedDescription()
    {
        await AuthenticateAsync();
        var task = new TaskViewModel
        {
            title = $"testing task updation",
            description = $"test description",
            expirationTime = DateTime.Now.AddDays(3)
        };

        HttpResponseMessage response = await CreateTaskAsync(task);
        CheckStatusCodeIfNotValidFailTest(response);

        IEnumerable<TaskViewModel> tasks = await GetTasksAsync();
        TaskViewModel? taskToUpdate = tasks.FirstOrDefault(x => x.title == task.title &&
            x.description == task.description);

        if (taskToUpdate == null)
            Assert.Fail("Task has not been created");

        string expectedDescription = "updated";
        task.description = expectedDescription;

        HttpResponseMessage updateResponse = await UpdateTask(taskToUpdate.id, task);
        CheckStatusCodeIfNotValidFailTest(updateResponse);

        TaskViewModel? updatedTask = await GetTaskAsync(taskToUpdate.id);

        if (updatedTask == null)
            Assert.Fail("Updated task can not be null");

        Assert.That(updatedTask.description, Is.EqualTo(expectedDescription));
    }
    [Test]
    public async Task DeleteTask_CreateThenDeleteTask_OkResponse()
    {
        await AuthenticateAsync();
        // Create task
        var task = new TaskViewModel
        {
            title = "to delete",
            description = "test description",
            expirationTime = DateTime.Now.AddDays(2)
        };

        HttpResponseMessage response = await CreateTaskAsync(task);
        CheckStatusCodeIfNotValidFailTest(response);

        IEnumerable<TaskViewModel> tasks = await GetTasksAsync();
        var taskToDelete = tasks.FirstOrDefault(x => x.title == task.title && x.description == task.description);

        if (taskToDelete == null)
            Assert.Fail("Task has not been created");

        // Delete task
        var responseFromDeleteMethod = await DeleteTask(taskToDelete.id);
        CheckStatusCodeIfNotValidFailTest(responseFromDeleteMethod);
    }
    [Test]
    public async Task DeleteTask_CreateThenDeleteTask_GetZeroTasks()
    {
        await DeleteTask_CreateThenDeleteTask_OkResponse();

        // Authenticate already have done in DeleteTask_CreateTask_OkResponse 
        var tasks = (await GetTasksAsync()).ToList();

        int expectedCountOfTasks = 0;
        int actualCountOfTasks = tasks.Count;

        Assert.That(expectedCountOfTasks, Is.EqualTo(actualCountOfTasks));
    }
}
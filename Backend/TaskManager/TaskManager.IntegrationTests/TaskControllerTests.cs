using System.Net;
using System.Text.Json;
using TaskManager.ViewModels;

namespace TaskManager.IntegrationTests;

public class TaskControllerTests : IntegrationTest
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

        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl + "Task");
        HttpStatusCode actualStatusCode = response.StatusCode;
        if(actualStatusCode != HttpStatusCode.OK)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }
        var actualTasks = await JsonSerializer
            .DeserializeAsync<List<TaskViewModel>>(await response.Content.ReadAsStreamAsync());

        var expectedStatusCode = HttpStatusCode.OK;
        var expectedTasks = new List<TaskViewModel> { };

        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
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
        var expectedStatusCode = HttpStatusCode.OK;
        foreach (var response in responses)
        {
            HttpStatusCode actualStatusCodeFromCreateMethod = response.StatusCode;

            if (actualStatusCodeFromCreateMethod != expectedStatusCode)
            {
                string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Assert.Fail(errorMessage);
            }
        }

        List<TaskViewModel> actualTasks = (await GetTasksAsync()).ToList();
        List<TaskViewModel> expectedTasks = tasks;

        foreach (var item in expectedTasks)
        {
            if(actualTasks.Exists(x => x.title == item.title && x.description == item.description 
                && item.expirationTime == x.expirationTime) == false)
            {
                Assert.Fail($"There are no item from server with title: {item.title} and desc:{item.description}");
            }
        }
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

        HttpStatusCode actualStatusCode = response.StatusCode;
        var expectedStatusCode = HttpStatusCode.OK;

        if (actualStatusCode != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }
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

        var response = await CreateTaskAsync(task);
        var expectedStatusCode = HttpStatusCode.OK;
        if(response.StatusCode != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }

        IEnumerable<TaskViewModel> tasks = await GetTasksAsync();

        TaskViewModel? taskToUpdate = tasks.FirstOrDefault(x => x.title == task.title && 
            x.description == task.description);

        if (taskToUpdate == null)
            Assert.Fail("Task are not in database");

        task.description = "updated";
        var updateResponse = await UpdateTask(taskToUpdate.id, task);
        if (updateResponse.StatusCode != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }

        Assert.Pass("Task was successfully updated");
    }
    [Test]
    public async Task UpdateTask_UpdateOperationGetUpdatedTask_UpdatedDescription()
    {
        await AuthenticateAsync();

        var task = new TaskViewModel
        {
            title = $"testing task updation",
            description = $"test description",
            expirationTime = DateTime.Now.AddDays(3)
        };

        var response = await CreateTaskAsync(task);
        var expectedStatusCode = HttpStatusCode.OK;
        if (response.StatusCode != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }

        IEnumerable<TaskViewModel> tasks = await GetTasksAsync();

        TaskViewModel? taskToUpdate = tasks.FirstOrDefault(x => x.title == task.title &&
            x.description == task.description);

        if (taskToUpdate == null)
            Assert.Fail("Task are not in database");

        string expectedDescription = "updated";

        task.description = expectedDescription;
        var updateResponse = await UpdateTask(taskToUpdate.id, task);
        if (updateResponse.StatusCode != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }

        var updatedTask = await GetTaskAsync(taskToUpdate.id);

        if (updatedTask == null)
            Assert.Fail("Updated task can not be null");

        Assert.That(updatedTask.description, Is.EqualTo(expectedDescription));
    }
    [Test]
    public async Task DeleteTask_CreateTask_OkResponse()
    {
        await AuthenticateAsync();
        var task = new TaskViewModel
        {
            title = "to delete",
            description = "test description",
            expirationTime = DateTime.Now.AddDays(2)
        };

        HttpResponseMessage response = await CreateTaskAsync(task);

        HttpStatusCode actualStatusCode = response.StatusCode;
        var expectedStatusCode = HttpStatusCode.OK;

        if (actualStatusCode != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }

        IEnumerable<TaskViewModel>? tasks = await GetTasksAsync();
        var taskToDelete = tasks.FirstOrDefault(x => x.title == task.title && x.description == task.description);
        
        if (taskToDelete == null)
            Assert.Fail("Task has not been created");

        var deleteResponse = await DeleteTask(taskToDelete.id);
        HttpStatusCode actualStatusCodeFromDeleteMethod = response.StatusCode;

        if (actualStatusCodeFromDeleteMethod != expectedStatusCode)
        {
            string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Fail(errorMessage);
        }
    }
    [Test]
    public async Task DeleteTask_CreateTask_GetZeroTasks()
    {
        await DeleteTask_CreateTask_OkResponse();

        // Authenticate already have done in DeleteTask_CreateTask_OkResponse 
        var tasks = (await GetTasksAsync()).ToList();

        int expectedCountOfTasks = 0;
        int actualCountOfTasks = tasks.Count;

        Assert.That(expectedCountOfTasks, Is.EqualTo(actualCountOfTasks));
    }
}
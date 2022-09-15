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

        Assert.AreEqual(expected, actual);
    }
    [Test]
    public async Task GetTasks_WithoutAnyPosts_EmptyResponse()
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

        Assert.AreEqual(expectedStatusCode, actualStatusCode);
        CollectionAssert.AreEqual(expectedTasks, actualTasks);
    }
}
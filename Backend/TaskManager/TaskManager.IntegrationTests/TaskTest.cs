using System.Net;
using System.Text;
using System.Text.Json;
using TaskManager.ViewModels;

namespace TaskManager.IntegrationTests;

public class TaskTest : IntegrationTest
{
    protected async Task<TaskViewModel?> GetTaskAsync(string taskId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl + "Task" + $"/{taskId}");
        CheckStatusCodeIfNotValidFailTest(response);

        return await JsonSerializer.DeserializeAsync<TaskViewModel>
            (await response.Content.ReadAsStreamAsync());
    }
    protected async Task<IEnumerable<TaskViewModel>> GetTasksAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl + "Task");
        CheckStatusCodeIfNotValidFailTest(response);

        var tasks = await JsonSerializer.DeserializeAsync<List<TaskViewModel>>
            (await response.Content.ReadAsStreamAsync());

        if (tasks == null)
            throw new NullReferenceException();
        return tasks;
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

using Domain;

namespace Services.Abstract;

public interface ITaskService
{
    public Task<IEnumerable<TaskDomain>> GetTasksAsync(string userId);
    public Task<TaskDomain> GetTaskAsync(string userId, string taskId);
    public Task CreateTaskAsync(string userId, TaskDomain task);
    public Task DeleteTaskAsync(string userId, string taskId);
    public Task UpdateTaskAsync(string userId, string taskId, TaskDomain task);
}
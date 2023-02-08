using TaskManager.Api.Domain;

namespace TaskManager.Api.Services.Abstract;

public interface ITaskService
{
    Task AddTaskAsync(Guid accountId, TaskDomain taskDomain);
    Task<List<TaskDomain>> GetTasksAsync(Guid accountId);
    Task<TaskDomain?> GetTaskByIdAsync(Guid accountId, Guid taskId);
    Task<bool> UpdateTaskAsync(Guid accountId, TaskDomain taskToUpdate);
    Task<bool> DeleteTaskAsync(Guid accountId, Guid taskId);
}
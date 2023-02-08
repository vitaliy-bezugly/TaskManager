using TaskManager.Api.Entities;

namespace TaskManager.Api.Repositories.Abstract;

public interface ITaskRepository
{
    Task AddTaskAsync(Guid accountId, TaskEntity taskEntity);
    Task<List<TaskEntity>> GetTasksAsync(Guid accountId);
    Task<TaskEntity?> GetTaskByIdAsync(Guid accountId, Guid taskId);
    Task<bool> UpdateTaskAsync(Guid accountId, TaskEntity taskToUpdate);
    Task<bool> DeleteTaskAsync(Guid accountId, Guid taskId);
}
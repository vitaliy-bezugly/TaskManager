using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Repositories.Abstract;

public interface ITaskRepository
{
    Task AddTaskAsync(TaskEntity taskEntity);
    Task<List<TaskEntity>> GetTasksAsync();
    Task<TaskEntity?> GetTaskByIdAsync(Guid taskId);
    Task<bool> UpdateTaskAsync(TaskEntity taskToUpdate);
    Task<bool> DeleteTaskAsync(Guid taskId);
}
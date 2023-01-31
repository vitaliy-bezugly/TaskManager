using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Services.Abstract;

public interface ITaskService
{
    Task AddTaskAsync(TaskDomain taskDomain);
    Task<List<TaskDomain>> GetTasksAsync();
    Task<TaskDomain?> GetTaskByIdAsync(Guid taskId);
    Task<bool> UpdateTaskAsync(TaskDomain taskToUpdate);
    Task<bool> DeleteTaskAsync(Guid taskId);
}
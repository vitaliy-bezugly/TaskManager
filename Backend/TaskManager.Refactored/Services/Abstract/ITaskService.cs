using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Services.Abstract;

public interface ITaskService
{
    void AddTask(TaskDomain taskDomain);
    IEnumerable<TaskDomain> GetTasks();
    TaskDomain? GetTaskById(Guid taskId);
    bool UpdateTask(TaskDomain taskToUpdate);
    bool DeleteTask(Guid taskId);
}
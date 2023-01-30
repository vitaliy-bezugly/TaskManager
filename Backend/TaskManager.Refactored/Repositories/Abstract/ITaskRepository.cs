using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Repositories.Abstract;

public interface ITaskRepository
{
    IEnumerable<TaskDomain> GetTasks();
    TaskDomain GetTaskById(Guid taskId);
    bool AddTask(TaskDomain taskDomain);
}

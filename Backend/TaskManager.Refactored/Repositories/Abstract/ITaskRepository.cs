using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Repositories.Abstract;

public interface ITaskRepository
{
    void AddTask(TaskEntity taskEntity);
    IEnumerable<TaskEntity> GetTasks();
    TaskEntity? GetTaskById(Guid taskId);
    bool UpdateTask(TaskEntity taskToUpdate);
    bool DeleteTask(Guid taskId);
}
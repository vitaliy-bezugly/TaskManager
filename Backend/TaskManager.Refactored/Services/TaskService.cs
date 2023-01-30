using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Services;

public class TaskService : ITaskService
{
    private List<TaskDomain> _tasks;
    public TaskService()
    {
        _tasks = new List<TaskDomain>();

        for (int i = 1; i <= 10; i++)
        {
            _tasks.Add(new TaskDomain { Title = $"Task #{i}"});
        }
    }

    public void AddTask(TaskDomain taskDomain)
    {
        _tasks.Add(taskDomain);
    }

    public TaskDomain? GetTaskById(Guid taskId)
    {
        return _tasks.FirstOrDefault(x => x.Id == taskId);
    }

    public IEnumerable<TaskDomain> GetTasks()
    {
        return _tasks;
    }

    public bool UpdateTask(TaskDomain taskToUpdate)
    {
        var task = GetTaskById(taskToUpdate.Id);

        if (task is null)
            return false;

        int index = _tasks.FindIndex(x => x.Id == taskToUpdate.Id);
        _tasks[index] = taskToUpdate;
        return true;
    }

    public bool DeleteTask(Guid taskId)
    {
        var task = GetTaskById(taskId);

        if (task is null)
            return false;

        _tasks.Remove(task);
        return true;
    }
}

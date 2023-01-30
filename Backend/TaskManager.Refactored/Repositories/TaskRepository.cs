using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Repositories;

public class TaskRepository : ITaskRepository
{
    private List<TaskDomain> _tasks;
    public TaskRepository()
    {
        _tasks = new List<TaskDomain>();

        for(int i = 1; i <= 10; i++)
        {
            _tasks.Add(new TaskDomain());
        }
    }

    public bool AddTask(TaskDomain taskDomain)
    {
        throw new NotImplementedException();
    }

    public TaskDomain GetTaskById(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskDomain> GetTasks()
    {
        throw new NotImplementedException();
    }
}

using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Repositories;

public class TaskRepository : ITaskRepository
{
    private List<TaskDomain> _tasks;
    private static Random random = new Random();

    public TaskRepository()
    {
        _tasks = new List<TaskDomain>();

        
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

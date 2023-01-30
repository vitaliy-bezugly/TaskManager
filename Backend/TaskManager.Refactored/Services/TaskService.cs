using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Services;

public class TaskService : ITaskService
{
    private List<TaskDomain> _tasks;
    private static Random random = new Random();

    public TaskService()
    {
        _tasks = new List<TaskDomain>();

        for (int i = 1; i <= 10; i++)
        {
            _tasks.Add(new TaskDomain
            {
                Title = $"Title: {i}",
                Description = RandomString(26),
                IsImportant = random.Next(1, 5) == 1,
                ExpirationTime = DateTime.Now.AddDays(random.Next(1, 360))
            });
        }
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
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
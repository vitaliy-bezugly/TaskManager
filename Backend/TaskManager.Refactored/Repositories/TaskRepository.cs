using TaskManager.Refactored.Entities;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Repositories;

public class TaskRepository : ITaskRepository
{
    private List<TaskEntity> _tasks;
    private static Random random = new Random();

    public TaskRepository()
    {
        _tasks = new List<TaskEntity>();

        for (int i = 1; i <= 10; i++)
        {
            _tasks.Add(new TaskEntity
            {
                Id = Guid.NewGuid(),
                Title = $"Title: {i}",
                Description = RandomString(26),
                IsImportant = random.Next(1, 5) == 1,
                CreationTime = DateTime.Now,
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

    public void AddTask(TaskEntity taskDomain)
    {
        _tasks.Add(taskDomain);
    }
    public TaskEntity? GetTaskById(Guid taskId)
    {
        return _tasks.FirstOrDefault(x => x.Id == taskId);
    }
    public IEnumerable<TaskEntity> GetTasks()
    {
        return _tasks;
    }
    public bool UpdateTask(TaskEntity taskToUpdate)
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

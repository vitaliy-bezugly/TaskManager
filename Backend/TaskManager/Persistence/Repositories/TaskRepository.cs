using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstract;

namespace Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;
    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateTaskAsync(string userId, TaskEntity task)
    {
        if (task == null)
            throw new ArgumentNullException("Argument can not be null" + nameof(task));

        UserEntity user = await RecognizeUserOrThrowExceptionAsync(userId);
        user.Tasks.Add(task);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(string userId, string taskId)
    {
        UserEntity user = await RecognizeUserOrThrowExceptionAsync(userId);

        TaskEntity? taskToDelete = user.Tasks.FirstOrDefault(x => x.Id == taskId);

        if (taskToDelete == null)
            throw new ArgumentException($"There is no task with index: {taskId}");
        user.Tasks.Remove(taskToDelete);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(string userId, string taskId, TaskEntity task)
    {
        UserEntity user = await RecognizeUserOrThrowExceptionAsync(userId);

        TaskEntity? taskToUpdate = user.Tasks.FirstOrDefault(x => x.Id == taskId);
        if (taskToUpdate == null)
            throw new ArgumentException($"There is no object with index: {taskId}");

        taskToUpdate.Title = task.Title;
        taskToUpdate.Description = task.Description;
        taskToUpdate.CreatedTime = task.CreatedTime;
        taskToUpdate.ExpirationTime = task.ExpirationTime;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksAsync(string userId)
    {
        UserEntity user = await RecognizeUserOrThrowExceptionAsync(userId);
        return user.Tasks;
    }

    public async Task<TaskEntity> GetTaskAsync(string userId, string taskId)
    {
        UserEntity user = await RecognizeUserOrThrowExceptionAsync(userId);
        TaskEntity? task = user.Tasks.FirstOrDefault(x => x.Id == taskId);

        if (task == null)
            throw new ArgumentException($"There is no object with index: {taskId}");

        return task;
    }

    private async Task<UserEntity> RecognizeUserOrThrowExceptionAsync(string userId)
    {
        var user = await _context.Users.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            throw new ArgumentException($"There is no user with id: {userId}");

        return user;
    }
}

using Microsoft.EntityFrameworkCore;
using TaskManager.Refactored.Entities;
using TaskManager.Refactored.Persistence;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDataContext _context;

    public TaskRepository(ApplicationDataContext context)
    {
        _context = context;
    }

    public async Task AddTaskAsync(TaskEntity taskDomain)
    {
        _context.Tasks.Add(taskDomain);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(Guid taskId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
    }
    public async Task<List<TaskEntity>> GetTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<bool> UpdateTaskAsync(TaskEntity taskToUpdate)
    {
        var task = await GetTaskByIdAsync(taskToUpdate.Id);

        if (task == null)
            return false;

        task.Title = taskToUpdate.Title;
        task.Description = taskToUpdate.Description;
        task.IsImportant = taskToUpdate.IsImportant;
        task.ExpirationTime = taskToUpdate.ExpirationTime;

        int updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
    public async Task<bool> DeleteTaskAsync(Guid taskId)
    {
        var task = await GetTaskByIdAsync(taskId);

        if (task is null)
            return false;

        _context.Tasks.Remove(task);
        int removed = await _context.SaveChangesAsync();

        return removed > 0;
    }
}

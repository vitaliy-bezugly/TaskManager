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

    public async Task AddTaskAsync(Guid accountId, TaskEntity taskEntity)
    {
        taskEntity.AccountId = accountId;
        await _context.Tasks.AddAsync(taskEntity);
        await _context.SaveChangesAsync();
    }
    public async Task<TaskEntity?> GetTaskByIdAsync(Guid accountId, Guid taskId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(x => x.AccountId == accountId && x.Id == taskId);
    }
    public async Task<List<TaskEntity>> GetTasksAsync(Guid accountId)
    {
        return await _context.Tasks.Where(x => x.AccountId == accountId).ToListAsync();
    }
    public async Task<bool> UpdateTaskAsync(Guid accountId, TaskEntity taskToUpdate)
    {
        var task = await GetTaskByIdAsync(accountId, taskToUpdate.Id);

        if (task == null)
            return false;

        task.Title = taskToUpdate.Title;
        task.Description = taskToUpdate.Description;
        task.IsImportant = taskToUpdate.IsImportant;
        task.ExpirationTime = taskToUpdate.ExpirationTime;

        int updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
    public async Task<bool> DeleteTaskAsync(Guid accountId, Guid taskId)
    {
        var task = await GetTaskByIdAsync(accountId, taskId);

        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        int removed = await _context.SaveChangesAsync();

        return removed > 0;
    }

    public async Task<AccountEntity?> GetAccountWithTasksById(Guid accountId)
    {
        return await _context.Accounts.Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == accountId);
    }
}

using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Entities;
using TaskManager.Refactored.Repositories.Abstract;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task AddTaskAsync(TaskDomain taskDomain)
    {
        await _taskRepository.AddTaskAsync(new TaskEntity
        {
            Id = taskDomain.Id,
            Title = taskDomain.Title,
            Description = taskDomain.Description,
            IsImportant = taskDomain.IsImportant,
            CreationTime = taskDomain.CreationTime,
            ExpirationTime = taskDomain.ExpirationTime
        });
    }
    public async Task<TaskDomain?> GetTaskByIdAsync(Guid taskId)
    {
        var taskEntity = await _taskRepository.GetTaskByIdAsync(taskId);

        if(taskEntity == null)
            return null;

        return new TaskDomain
        {
            Id = taskEntity.Id,
            Title = taskEntity.Title,
            Description = taskEntity.Description,
            IsImportant = taskEntity.IsImportant,
            CreationTime = taskEntity.CreationTime,
            ExpirationTime = taskEntity.ExpirationTime
        };
    }
    public async Task<List<TaskDomain>> GetTasksAsync()
    {
        var tasks = await _taskRepository.GetTasksAsync();
        return tasks.Select(x =>
        {
            return new TaskDomain
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsImportant = x.IsImportant,
                CreationTime = x.CreationTime,
                ExpirationTime = x.ExpirationTime
            };
        }).ToList();
    }
    public async Task<bool> UpdateTaskAsync(TaskDomain taskToUpdate)
    {
        return await _taskRepository.UpdateTaskAsync(new TaskEntity
        {
            Id = taskToUpdate.Id,
            Title = taskToUpdate.Title,
            Description = taskToUpdate.Description,
            IsImportant = taskToUpdate.IsImportant,
            CreationTime = taskToUpdate.CreationTime,
            ExpirationTime = taskToUpdate.ExpirationTime
        });
    }
    public async Task<bool> DeleteTaskAsync(Guid taskId)
    {
        return await _taskRepository.DeleteTaskAsync(taskId);
    }
}
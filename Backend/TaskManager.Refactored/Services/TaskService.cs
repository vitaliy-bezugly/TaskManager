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

    public void AddTask(TaskDomain taskDomain)
    {
        _taskRepository.AddTask(new TaskEntity
        {
            Id = taskDomain.Id,
            Title = taskDomain.Title,
            Description = taskDomain.Description,
            IsImportant = taskDomain.IsImportant,
            CreationTime = taskDomain.CreationTime,
            ExpirationTime = taskDomain.ExpirationTime
        });
    }
    public TaskDomain? GetTaskById(Guid taskId)
    {
        var taskEntity = _taskRepository.GetTaskById(taskId);

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
    public IEnumerable<TaskDomain> GetTasks()
    {
        return _taskRepository.GetTasks().Select(x =>
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
        });
    }
    public bool UpdateTask(TaskDomain taskToUpdate)
    {
        return _taskRepository.UpdateTask(new TaskEntity
        {
            Id = taskToUpdate.Id,
            Title = taskToUpdate.Title,
            Description = taskToUpdate.Description,
            IsImportant = taskToUpdate.IsImportant,
            CreationTime = taskToUpdate.CreationTime,
            ExpirationTime = taskToUpdate.ExpirationTime
        });
    }
    public bool DeleteTask(Guid taskId)
    {
        return _taskRepository.DeleteTask(taskId);
    }
}
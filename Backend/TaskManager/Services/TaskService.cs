using Mappers;
using Services.Abstract;
using Persistence.Repositories.Abstract;
using Microsoft.Extensions.Logging;

namespace Domain.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly ILogger<TaskService> _logger;
    public TaskService(ITaskRepository repository, ILogger<TaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateTaskAsync(string userId, TaskDomain task)
    {
        try
        {
            await _repository.CreateTaskAsync(userId, task.ToEntity());
        }
        catch (Exception e)
        {
            _logger.LogError($"Can not create task", e);
            throw e;
        }
    }

    public async Task DeleteTaskAsync(string userId, string taskId)
    {
        try
        {
            await _repository.DeleteTaskAsync(userId, taskId);
        }
        catch (Exception e)
        {
            _logger.LogError($"Can not delete task with index: {taskId}", e);
            throw e;
        }
    }

    public async Task UpdateTaskAsync(string userId, string taskId, TaskDomain task)
    {
        try
        {
            await _repository.UpdateTaskAsync(userId, taskId, task.ToEntity());
        }
        catch (Exception e)
        {
            _logger.LogError($"Can not update task with index: {taskId}", e);
            throw e;
        }
    }

    public async Task<IEnumerable<TaskDomain>> GetTasksAsync(string userId)
    {
        var tasks = await _repository.GetTasksAsync(userId);
        return tasks.Select(x => x.ToDomain());
    }

    public async Task<TaskDomain> GetTaskAsync(string userId, string taskId)
    {
        try
        {
            var task = await _repository.GetTaskAsync(userId, taskId);
            return task.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError($"Can not find task with index: {taskId}", e);
            throw e;
        }
    }
}

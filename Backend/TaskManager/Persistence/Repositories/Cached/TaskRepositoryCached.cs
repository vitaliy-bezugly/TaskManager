using Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Persistence.Extensions;
using Persistence.Repositories.Abstract;

namespace Persistence.Repositories.Cached;

public class TaskRepositoryCached : ITaskRepository
{
    private readonly ITaskRepository _taskRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<TaskRepositoryCached> _logger;
    public TaskRepositoryCached(ITaskRepository taskRepository, IDistributedCache cache, 
        ILogger<TaskRepositoryCached> logger, IAccountRepository accountRepository)
    {
        _taskRepository = taskRepository;
        _cache = cache;
        _logger = logger;
        _accountRepository = accountRepository;
    }

    public async Task CreateTaskAsync(string userId, TaskEntity task)
    {
        var resultFromCache = await _cache.GetRecordAsync<IEnumerable<TaskEntity>>
            (GetKeyForCache(userId));

        if (resultFromCache != null)
        {
            await _cache.SetRecordAsync<IEnumerable<TaskEntity>>(GetKeyForCache(userId), null);
        }

        await _taskRepository.CreateTaskAsync(userId, task);
    }

    public async Task DeleteTaskAsync(string userId, string taskId)
    {
        TaskEntity itemFromCache = await _cache.GetRecordAsync<TaskEntity>
            (GetKeyForCache(userId, taskId));
        IEnumerable<TaskEntity> itemsFromCache = await _cache.GetRecordAsync<IEnumerable<TaskEntity>>
            (GetKeyForCache(userId));
        if (itemFromCache != null)
        {
            await _cache.SetRecordAsync<TaskEntity>(GetKeyForCache(userId, taskId), null);
        }

        if(itemsFromCache != null)
        {
            await _cache.SetRecordAsync<TaskEntity>(GetKeyForCache(userId), null);
        }

        await _taskRepository.DeleteTaskAsync(userId, taskId);
    }

    public async Task<TaskEntity> GetTaskAsync(string userId, string taskId)
    {
        TaskEntity resultFromCache = await _cache.GetRecordAsync<TaskEntity>
            (GetKeyForCache(userId, taskId));

        if (resultFromCache == null)
        {
            _logger.LogInformation($"There is no task with id: {taskId} in cache");
            var task = await _taskRepository.GetTaskAsync(userId, taskId);
            await _cache.SetRecordAsync<TaskEntity>(GetKeyForCache(userId, taskId), task);

            return task;
        }

        _logger.LogInformation($"Task with id: {taskId} has been returned from cache");
        return resultFromCache;
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksAsync(string userId)
    {
        var resultFromCache = await _cache.GetRecordAsync<IEnumerable<TaskEntity>>
            (GetKeyForCache(userId));

        if (resultFromCache == null)
        {
            _logger.LogInformation($"There are no user({userId}) tasks in cache. Getting it from db");

            var tasks = await _taskRepository.GetTasksAsync(userId);
            await _cache.SetRecordAsync<IEnumerable<TaskEntity>>(GetKeyForCache(userId), tasks);

            return tasks;
        }

        _logger.LogInformation($"There are users({userId}) tasks in cache.");
        return resultFromCache;
    }

    public async Task UpdateTaskAsync(string userId, string taskId, TaskEntity task)
    {
        TaskEntity resultFromCache = await _cache.GetRecordAsync<TaskEntity>
            (GetKeyForCache(userId, taskId));

        if (resultFromCache != null)
        {
            await _cache.SetRecordAsync<TaskEntity>(GetKeyForCache(userId, taskId), task);
        }

        await _taskRepository.UpdateTaskAsync(userId, taskId, task);
    }

    private string GetKeyForCache(string userId, string taskId = "all") => $"{userId}:{taskId}";
}

using AutoMapper;
using TaskManager.Api.Domain;
using TaskManager.Api.Entities;
using TaskManager.Api.Repositories.Abstract;
using TaskManager.Api.Services.Abstract;

namespace TaskManager.Api.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task AddTaskAsync(Guid accountId, TaskDomain taskDomain)
    {
        await _taskRepository.AddTaskAsync(accountId, _mapper.Map<TaskEntity>(taskDomain));
    }
    public async Task<TaskDomain?> GetTaskByIdAsync(Guid accountId, Guid taskId)
    {
        var taskEntity = await _taskRepository.GetTaskByIdAsync(accountId, taskId);

        if(taskEntity == null)
            return null;

        return _mapper.Map<TaskDomain>(taskEntity);
    }
    public async Task<List<TaskDomain>> GetTasksAsync(Guid accountId)
    {
        var tasks = await _taskRepository.GetTasksAsync(accountId);
        return tasks.Select(x => _mapper.Map<TaskDomain>(x)).ToList();
    }
    public async Task<bool> UpdateTaskAsync(Guid accountId, TaskDomain taskToUpdate)
    {
        return await _taskRepository.UpdateTaskAsync(accountId, _mapper.Map<TaskEntity>(taskToUpdate));
    }
    public async Task<bool> DeleteTaskAsync(Guid accountId, Guid taskId)
    {
        return await _taskRepository.DeleteTaskAsync(accountId, taskId);
    }
}
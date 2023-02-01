using AutoMapper;
using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Entities;
using TaskManager.Refactored.Repositories.Abstract;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task AddTaskAsync(TaskDomain taskDomain)
    {
        await _taskRepository.AddTaskAsync(_mapper.Map<TaskEntity>(taskDomain));
    }
    public async Task<TaskDomain?> GetTaskByIdAsync(Guid taskId)
    {
        var taskEntity = await _taskRepository.GetTaskByIdAsync(taskId);

        if(taskEntity == null)
            return null;

        return _mapper.Map<TaskDomain>(taskEntity);
    }
    public async Task<List<TaskDomain>> GetTasksAsync()
    {
        var tasks = await _taskRepository.GetTasksAsync();
        return tasks.Select(x => _mapper.Map<TaskDomain>(x)).ToList();
    }
    public async Task<bool> UpdateTaskAsync(TaskDomain taskToUpdate)
    {
        return await _taskRepository.UpdateTaskAsync(_mapper.Map<TaskEntity>(taskToUpdate));
    }
    public async Task<bool> DeleteTaskAsync(Guid taskId)
    {
        return await _taskRepository.DeleteTaskAsync(taskId);
    }
}
using Domain;
using Domain.Services.Abstract;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;
    private readonly string _userId;
    public TaskController(ITaskService taskService, ILogger<TaskController> logger)
    {
        _userId = "0f5430f3-20e1-473a-8637-75565dd16256";
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasksAsync()
    {
        try
        {
            IEnumerable<TaskDomain>? tasksDomain = await _taskService.GetTasksAsync(_userId);
            return Ok(tasksDomain.Select(x => x.ToViewModel()));
        }
        catch (Exception)
        {
            _logger.LogError("Can not get tasks");
            return BadRequest();
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskAsync(string id)
    {
        try
        {
            var task = await _taskService.GetTaskAsync(_userId, id);
            return Ok(task.ToViewModel());
        }
        catch (Exception)
        {
            _logger.LogError($"Can not get tasks with index: {id}");
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync(TaskViewModel task)
    {
        try
        {
            await _taskService.CreateTaskAsync(_userId, task.ToDomain());
            return Ok();
        }
        catch (Exception)
        {
            _logger.LogError($"Can not create task");
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskAsync(string id)
    {
        try
        {
            await _taskService.DeleteTaskAsync(_userId, id);
            return Ok();
        }
        catch (Exception)
        {
            _logger.LogError($"Can not delete task with index: {id}");
            return BadRequest();
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTaskAsync(string id, TaskViewModel task)
    {
        try
        {
            await _taskService.UpdateTaskAsync(_userId, id, task.ToDomain());
            return Ok();
        }
        catch (Exception)
        {
            _logger.LogError($"Can not update task with index: {id}");
            return BadRequest();
        }
    }
}

using Domain;
using Domain.Services.Abstract;
using Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.ViewModels;

namespace TaskManager.Controllers;

[ApiController, Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;
    private readonly string _userId;
    public TaskController(ITaskService taskService, ILogger<TaskController> logger,
         IHttpContextAccessor httpContextAccessor)
    {
        _taskService = taskService;
        _logger = logger;
        _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasksAsync()
    {
        try
        {
            IEnumerable<TaskDomain>? tasksDomain = await _taskService.GetTasksAsync(_userId);
            return Ok(tasksDomain.Select(x => x.ToViewModel()));
        }
        catch (Exception e)
        {
            _logger.LogError("Can not get tasks", e);
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
        catch (Exception e)
        {
            _logger.LogError($"Can not get tasks with index: {id}", e);
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
        catch (Exception e)
        {
            _logger.LogError($"Can not create task", e);
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
        catch (Exception e)
        {
            _logger.LogError($"Can not delete task with index: {id}", e);
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
        catch (Exception e)
        {
            _logger.LogError($"Can not update task with index: {id}", e);
            return BadRequest();
        }
    }
}

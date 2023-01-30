using Microsoft.AspNetCore.Mvc;
using TaskManager.Refactored.Contracts.v1;
using TaskManager.Refactored.Contracts.v1.Requests;
using TaskManager.Refactored.Contracts.v1.Responses;
using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Controllers.v1;

[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost, Route(ApiRoutes.Task.Create)]
    public IActionResult Create([FromBody] CreateTaskRequest taskRequest)
    {
        var task = new TaskDomain 
        { 
            Title = taskRequest.title, 
            Description = taskRequest.description,
            IsImportant = taskRequest.isImportant,
            ExpirationTime = taskRequest.expirationTime
        };

        _taskService.AddTask(task);

        var baseUrl = $"{HttpContext.Request.Scheme}//{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Task.Get.Replace("{taskId}", task.Id.ToString())}";

        var response = new CreateTaskResponse { Id = task.Id, Title = task.Title };
        return Created(locationUrl, response);
    }

    [HttpGet, Route(ApiRoutes.Task.GetAll)]
    public IActionResult GetAll()
    {
        IEnumerable<TaskDomain> tasks = _taskService.GetTasks();
        IEnumerable<GetTaskResponse> responses = tasks.Select(x =>
        {
            return new GetTaskResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsImportant = x.IsImportant,
                CreationTime = x.CreationTime,
                ExpirationTime = x.ExpirationTime
            };
        });

        return Ok(responses);
    }
    [HttpGet, Route(ApiRoutes.Task.Get)]
    public IActionResult Get([FromRoute] Guid taskId)
    {
        var task = _taskService.GetTaskById(taskId);

        if (task == null)
            return NotFound();

        var response = new GetTaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationTime = task.CreationTime,
            ExpirationTime = task.ExpirationTime
        };

        return Ok(response);
    }

    [HttpPut, Route(ApiRoutes.Task.Update)]
    public IActionResult Update([FromRoute] Guid taskId, [FromBody] UpdateTaskRequest taskRequest)
    {
        var task = new TaskDomain
        {
            Id = taskId,
            Title = taskRequest.title,
            Description = taskRequest.description,
            IsImportant = taskRequest.isImportant,
            ExpirationTime = taskRequest.expirationTime
        };

        bool result = _taskService.UpdateTask(task);

        if(result == false)
            return NotFound();

        var response = new UpdateTaskResponse
        {
            Id = task.Id,
            Title = task.Title
        };

        return Ok(response);
    }

    [HttpDelete, Route(ApiRoutes.Task.Delete)]
    public IActionResult Delete([FromRoute] Guid taskId)
    {
        bool result = _taskService.DeleteTask(taskId);

        if (result == false)
            return NotFound();

        return NoContent();
    }
}
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
        var task = new TaskDomain { Id = taskRequest.Id, Title = taskRequest.Title };
        _taskService.AddTask(task);

        var baseUrl = $"{HttpContext.Request.Scheme}//{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Task.Get.Replace("{taskId}", task.Id.ToString())}";

        var response = new CreateTaskResponse { Id = task.Id, Title = task.Title };
        return Created(locationUrl, response);
    }

    [HttpGet, Route(ApiRoutes.Task.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_taskService.GetTasks());
    }
    [HttpGet, Route(ApiRoutes.Task.Get)]
    public IActionResult Get([FromRoute] Guid taskId)
    {
        var task = _taskService.GetTaskById(taskId);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPut, Route(ApiRoutes.Task.Update)]
    public IActionResult Update([FromRoute] Guid taskId, [FromBody] UpdateTaskRequest taskRequest)
    {
        var task = new TaskDomain
        {
            Id = taskId,
            Title = taskRequest.Title
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
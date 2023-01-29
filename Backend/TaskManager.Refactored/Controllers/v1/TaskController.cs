using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Refactored.Contracts.v1;
using TaskManager.Refactored.Contracts.v1.Requests;
using TaskManager.Refactored.Contracts.v1.Responses;
using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Controllers.v1;

[ApiController]
public class TaskController : ControllerBase
{
    private List<TaskDomain> _tasks;

    public TaskController()
    {
        _tasks = new List<TaskDomain>();

        for (int i = 1; i <= 5; ++i)
            _tasks.Add(new TaskDomain());
    }

    [HttpGet, Route(ApiRoutes.Task.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_tasks);
    }

    [HttpPost, Route(ApiRoutes.Task.Create)]
    public IActionResult Create([FromBody] CreateTaskRequest taskRequest)
    {
        var task = new TaskDomain { Id = taskRequest.Id };

        var baseUrl = $"{HttpContext.Request.Scheme}//{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Task.Get.Replace("{taskId}", task.Id)}";

        var response = new CreateTaskResponse { Id = task.Id };
        return Created(locationUrl, response);
    }
}
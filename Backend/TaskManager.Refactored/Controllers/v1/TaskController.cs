using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Refactored.Contracts.v1;
using TaskManager.Refactored.Contracts.v1.Requests;
using TaskManager.Refactored.Contracts.v1.Responses;
using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Controllers.v1;

[ApiController, Authorize]
public class TaskController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;
    private readonly string _currentAccountId;
    public TaskController(ITaskService taskService, IMapper mapper, IHttpContextAccessor httpContextAccessor
        , ILogger<TaskController> logger)
    {
        _taskService = taskService;
        _mapper = mapper;
        _currentAccountId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        _logger = logger;
    }

    [HttpPost, Route(ApiRoutes.Task.Create)]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest taskRequest)
    {
        var task = _mapper.Map<TaskDomain>(taskRequest);

        await _taskService.AddTaskAsync(task);

        var baseUrl = $"{HttpContext.Request.Scheme}//{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Task.Get.Replace("{taskId}", task.Id.ToString())}";

        var response = _mapper.Map<CreateTaskResponse>(task);
        return Created(locationUrl, response);
    }

    [HttpGet, Route(ApiRoutes.Task.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("User id: ", _currentAccountId);

        List<TaskDomain> tasks = await _taskService.GetTasksAsync();
        IEnumerable<GetTaskResponse> responses = tasks.Select(x => _mapper.Map<GetTaskResponse>(x));

        return Ok(responses);
    }
    [HttpGet, Route(ApiRoutes.Task.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid taskId)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);

        if (task == null)
            return NotFound();

        var response = _mapper.Map<GetTaskResponse>(task);
        return Ok(response);
    }

    [HttpPut, Route(ApiRoutes.Task.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] UpdateTaskRequest taskRequest)
    {
        var task = _mapper.Map<TaskDomain>(taskRequest);
        task.Id = taskId;

        bool result = await _taskService.UpdateTaskAsync(task);

        if(result == false)
            return NotFound();

        var response = _mapper.Map<UpdateTaskResponse>(task);
        return Ok(response);
    }

    [HttpDelete, Route(ApiRoutes.Task.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid taskId)
    {
        bool result = await _taskService.DeleteTaskAsync(taskId);

        if (result == false)
            return NotFound();

        return NoContent();
    }
}
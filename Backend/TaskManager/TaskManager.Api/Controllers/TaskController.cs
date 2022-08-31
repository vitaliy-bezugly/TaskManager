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

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TaskViewModel>> GetTasks()
    {
        return Ok(_taskService.GetTasks().Select(x => x.ToViewModel()));
    }
    [HttpGet("{id:int}")]
    public ActionResult<TaskViewModel> GetTasks(int id)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public ActionResult CreateTask(TaskViewModel task)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("{id:int}")]
    public ActionResult DeleteTask()
    {
        throw new NotImplementedException();
    }
}

using Domain;
using Domain.Services.Abstract;
using Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Entities;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly ITaskService _taskService;

        public Test(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskViewModel>> GetTasks()
        {
            return Ok(_taskService.GetTasks().Select(x => x.ToViewModel()));
        }
    }
}

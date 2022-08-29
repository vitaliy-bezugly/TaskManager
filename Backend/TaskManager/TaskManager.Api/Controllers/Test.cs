using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Test(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskEntity>> GetTasks()
        {
            return Ok(_context.Tasks);
        }
    }
}

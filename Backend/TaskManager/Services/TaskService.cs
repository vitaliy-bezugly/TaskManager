using Persistence;
using Mappers;
using Domain.Services.Abstract;

namespace Domain.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TaskDomain> GetTasks()
    {
        return _context.Tasks.Select(x => x.ToDomain());
    }
}

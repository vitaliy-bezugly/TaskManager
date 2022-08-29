namespace Domain.Services.Abstract;

public interface ITaskService
{
    public IEnumerable<TaskDomain> GetTasks();
}
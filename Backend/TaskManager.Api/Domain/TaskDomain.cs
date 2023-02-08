using TaskManager.Api.Domain.Abstract;

namespace TaskManager.Api.Domain;

public class TaskDomain : BaseDomain
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplited { get; set; }
    public DateTime ExpirationTime { get; set; }
}
namespace Domain;

public class TaskDomain
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ExpirationTime { get; set; }
    public DateTime CreatedTime { get; set; }
}

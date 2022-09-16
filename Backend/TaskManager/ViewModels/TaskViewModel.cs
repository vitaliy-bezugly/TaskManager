namespace TaskManager.ViewModels;

public class TaskViewModel
{
    public string? id { get; set; }
    public string? title { get; set; }
    public string? description { get; set; }
    public DateTime expirationTime { get; set; }
    public DateTime createdTime { get; set; }
}

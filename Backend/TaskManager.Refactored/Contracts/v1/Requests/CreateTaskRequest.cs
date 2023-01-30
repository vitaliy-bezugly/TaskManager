using System.ComponentModel.DataAnnotations;

namespace TaskManager.Refactored.Contracts.v1.Requests;

public class CreateTaskRequest
{
    [Required]
    public string title { get; set; }
    public string? description { get; set; }
    public bool isImportant { get; set; }
    [Required]
    public DateTime expirationTime { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Contracts.v1.Requests;

public class CreateTaskRequest
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsImportant { get; set; }
    [Required]
    public DateTime ExpirationTime { get; set; }
}

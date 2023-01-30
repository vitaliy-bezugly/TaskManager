using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Refactored.Entities.Abstract;

namespace TaskManager.Refactored.Entities;

public class TaskEntity : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Column("Important")]
    public bool IsImportant { get; set; }
    [Required]
    public DateTime ExpirationTime { get; set; }
}
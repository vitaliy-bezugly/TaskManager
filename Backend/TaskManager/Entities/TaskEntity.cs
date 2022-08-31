using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persistence.Entities.Abstract;

namespace Entities;

[Table("Task")]
public class TaskEntity : BaseEntity
{
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime ExpirationTime { get; set; }

    [Required]
    public string UserId { get; set; }
    public UserEntity? User { get; set; }
}
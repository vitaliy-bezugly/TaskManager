using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Refactored.Entities.Abstract;

namespace TaskManager.Refactored.Entities;

[Table("Account")]
public class AccountEntity : BaseEntity
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }

    public ICollection<RoleEntity>? Roles { get; set; }
    public ICollection<TaskEntity>? Tasks { get; set; }
}
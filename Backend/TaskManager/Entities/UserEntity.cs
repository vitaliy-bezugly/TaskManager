using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persistence.Entities.Abstract;

namespace Entities;

[Table("User")]
public class UserEntity : BaseEntity
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
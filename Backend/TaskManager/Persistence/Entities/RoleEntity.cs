using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persistence.Entities.Abstract;

namespace Persistence.Entities;

[Table("Role")]
public class RoleEntity : BaseEntity
{
    public string? Role { get; set; }
    public ICollection<UserEntity>? Users { get; set; }
}
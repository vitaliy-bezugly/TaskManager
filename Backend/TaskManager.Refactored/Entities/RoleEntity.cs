using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Refactored.Entities.Abstract;

namespace TaskManager.Refactored.Entities;

[Table("Role")]
public class RoleEntity : BaseEntity
{
    public string? Role { get; set; }
    public ICollection<AccountEntity>? Accounts { get; set; }
}
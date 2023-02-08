using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Entities.Abstract;

namespace TaskManager.Api.Entities;

[Table("Role")]
public class RoleEntity : BaseEntity
{
    public string? Role { get; set; }
    public ICollection<AccountEntity>? Accounts { get; set; }
}
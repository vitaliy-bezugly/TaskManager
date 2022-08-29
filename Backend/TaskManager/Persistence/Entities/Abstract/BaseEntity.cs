using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities.Abstract;

public abstract class BaseEntity : IBaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}
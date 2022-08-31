using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities.Abstract;

public abstract class BaseEntity : IBaseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}
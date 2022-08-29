namespace Persistence.Entities.Abstract;

public interface IBaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
}
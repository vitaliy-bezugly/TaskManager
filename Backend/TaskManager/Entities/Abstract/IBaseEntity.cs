namespace Persistence.Entities.Abstract;

public interface IBaseEntity
{
    public string Id { get; set; }
    public DateTime CreatedTime { get; set; }
}
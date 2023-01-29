namespace TaskManager.Refactored.Domain.Abstract
{
    public abstract class DomainObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
namespace TaskManager.Refactored.Domain.Abstract
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
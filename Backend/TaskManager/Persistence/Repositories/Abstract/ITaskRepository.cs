using Entities;

namespace Persistence.Repositories.Abstract
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskEntity>> GetTasksAsync(string userId);
        public Task<TaskEntity> GetTaskAsync(string userId, string taskId);
        public Task CreateTaskAsync(string userId, TaskEntity task);
        public Task DeleteTaskAsync(string userId, string taskId);
        public Task UpdateTaskAsync(string userId, string taskId, TaskEntity task);
    }
}

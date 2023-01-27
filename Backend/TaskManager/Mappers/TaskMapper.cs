using Domain;
using Entities;
using TaskManager.ViewModels;

namespace Mappers;

public static class TaskMapper
{
    public static TaskEntity ToEntity(this TaskDomain task)
    {
        return new TaskEntity { Title = task.Title, Description = task.Description, 
            CreatedTime = task.CreatedTime, ExpirationTime = task.ExpirationTime };
    }

    public static TaskDomain ToDomain(this TaskViewModel task)
    {
        return new TaskDomain { Title = task.title, Description = task.description, 
             ExpirationTime = task.expirationTime, CreatedTime = task.createdTime };
    }

    public static TaskViewModel ToViewModel(this TaskDomain task)
    {
        return new TaskViewModel { id = task.Id, title = task.Title, description = task.Description, 
             expirationTime = task.ExpirationTime, createdTime = task.CreatedTime };
    }

    public static TaskDomain ToDomain(this TaskEntity task)
    {
        return new TaskDomain { Id = task.Id, Title = task.Title, Description = task.Description, 
             ExpirationTime = task.ExpirationTime, CreatedTime = task.CreatedTime };
    }
}

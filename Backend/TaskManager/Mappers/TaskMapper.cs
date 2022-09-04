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
        return new TaskDomain { Title = task.Title, Description = task.Description, 
             ExpirationTime = task.ExpirationTime };
    }

    public static TaskViewModel ToViewModel(this TaskDomain task)
    {
        return new TaskViewModel { Id = task.Id, Title = task.Title, Description = task.Description, 
             ExpirationTime = task.ExpirationTime, CreatedTime = task.CreatedTime };
    }

    public static TaskDomain ToDomain(this TaskEntity task)
    {
        return new TaskDomain { Id = task.Id, Title = task.Title, Description = task.Description, 
             ExpirationTime = task.ExpirationTime, CreatedTime = task.CreatedTime };
    }
}

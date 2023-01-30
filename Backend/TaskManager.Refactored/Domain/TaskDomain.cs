﻿using TaskManager.Refactored.Domain.Abstract;

namespace TaskManager.Refactored.Domain;

public class TaskDomain : BaseDomain
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsImportant { get; set; }
    public DateTime ExpirationTime { get; set; }
}

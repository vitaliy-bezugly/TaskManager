﻿namespace TaskManager.Refactored.Contracts.v1.Responses;

public class CreateTaskResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}

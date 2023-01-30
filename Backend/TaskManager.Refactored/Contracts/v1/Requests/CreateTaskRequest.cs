﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Refactored.Contracts.v1.Requests;

public class CreateTaskRequest
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
}

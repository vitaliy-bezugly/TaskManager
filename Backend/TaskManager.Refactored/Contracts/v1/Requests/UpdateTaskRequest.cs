﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Refactored.Contracts.v1.Requests;

public class UpdateTaskRequest
{
    [Required]
    public string Title { get; set; }
}

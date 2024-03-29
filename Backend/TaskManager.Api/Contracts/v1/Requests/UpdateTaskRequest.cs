﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Contracts.v1.Requests;

public class UpdateTaskRequest
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public bool IsImportant { get; set; }
    [Required]
    public bool IsComplited { get; set; }
    [Required]
    public DateTime ExpirationTime { get; set; }
}

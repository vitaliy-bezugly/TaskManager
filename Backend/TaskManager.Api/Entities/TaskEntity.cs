﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Entities.Abstract;

namespace TaskManager.Api.Entities;

[Table("Task")]
public class TaskEntity : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Column("Important")]
    public bool IsImportant { get; set; }
    [Column("Completed")]
    public bool IsComplited { get; set; }
    [Required]
    public DateTime ExpirationTime { get; set; }
    public AccountEntity? Account { get; set; }
    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }
}
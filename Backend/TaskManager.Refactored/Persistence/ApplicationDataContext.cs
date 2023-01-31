using Microsoft.EntityFrameworkCore;
using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Persistence;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
        : base(options)
    { }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
}

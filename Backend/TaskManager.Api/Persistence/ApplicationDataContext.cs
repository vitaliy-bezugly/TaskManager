using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Entities;

namespace TaskManager.Api.Persistence;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
        : base(options)
    { }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
    }
}

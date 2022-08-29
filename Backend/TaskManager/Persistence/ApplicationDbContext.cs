using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}
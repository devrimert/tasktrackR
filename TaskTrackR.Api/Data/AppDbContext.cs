using TaskTrackR.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackR.Api.Data;

//Bridge between code and the DB
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // store enums as strings => easier to read
        modelBuilder.Entity<TaskItem>()
        .Property(t=> t.Status)
        .HasConversion<string>();

        modelBuilder.Entity<TaskItem>()
        .Property(t=> t.Priority)
        .HasConversion<string>();
        
    }
}

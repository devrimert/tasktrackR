using TaskTrackR.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackR.Api.Data;

//Bridge between code and the DB
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}

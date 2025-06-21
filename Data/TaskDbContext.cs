using Microsoft.EntityFrameworkCore;
using TaskMasterApi.Models;

namespace TaskMasterApi.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }
        
        public DbSet<TaskItem>Tasks{ get; set; }
    }
}


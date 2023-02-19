using FireAndForgetHandler.Model;
using Microsoft.EntityFrameworkCore;

namespace FireAndForgetHandler.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskStatusInfo> TasksStatusInfo { get; set; }
    }
}

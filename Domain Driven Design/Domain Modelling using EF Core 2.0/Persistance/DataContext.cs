using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Persistance.Mapping;

namespace Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BacklogItemStatus> BacklogItemStatuses { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BacklogItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BacklogItemStatusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskStatusEntityConfiguration());
        }
    }
}
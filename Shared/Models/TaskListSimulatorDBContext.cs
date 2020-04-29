namespace TaskListSimulator.Shared.Models
{
    using System.Data.Entity;

    public class TaskListSimulatorDBContext : DbContext
    {

        public TaskListSimulatorDBContext(string connectionString) : base(connectionString)
        {

        }

        public DbSet<TasksEntity> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<TasksEntity>()
             .HasKey(key => key.TaskId);
        }
    }
}

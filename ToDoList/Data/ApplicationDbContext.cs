using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {   

        }

        public DbSet<MyTask>Task { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Status>Statuses { get; set; }

    }
}

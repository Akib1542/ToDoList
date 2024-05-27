using Microsoft.EntityFrameworkCore;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DatabaseAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
         //base.OnModelCreating(builder);
        public DbSet<MyTask>Task { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Status>Statuses { get; set; }
        public DbSet<ApplicationUser>ApplicationUsers { get; set; }
    }
}

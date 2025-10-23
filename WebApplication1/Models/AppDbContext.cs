using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }            // singular
        public DbSet<BloodRequest> BloodRequests { get; set; } // plural

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("STUDENT");
            modelBuilder.Entity<BloodRequest>().ToTable("BLOODREQUESTS"); // make sure table exists
        }
    }
}

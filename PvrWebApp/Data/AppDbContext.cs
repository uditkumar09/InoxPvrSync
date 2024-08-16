using PvrWebApp.Model;
using Microsoft.EntityFrameworkCore;
namespace PvrWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options)
        {

        }
        public DbSet<PvrUser> PvrUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PvrUser>().ToTable("PvrUsers");
        }

    }
}

using InoxWebApp.Model;
using Microsoft.EntityFrameworkCore;
namespace InoxWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options)
        {

        }
        public DbSet<InoxUser> InoxUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InoxUser>().ToTable("InoxUsers");
        }
    }
}

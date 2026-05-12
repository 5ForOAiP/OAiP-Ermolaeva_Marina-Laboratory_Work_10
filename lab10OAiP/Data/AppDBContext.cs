using lab10OAiP.Models;
using Microsoft.EntityFrameworkCore;


namespace lab10OAiP.Data
{
        public class AppDbContext : DbContext
        {
            public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=students.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}


    
        
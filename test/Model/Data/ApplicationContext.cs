using Microsoft.EntityFrameworkCore;

namespace test.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Tags> Tags { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestingDB;Trusted_Connection=true;");
        }
    }
}

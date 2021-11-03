using Microsoft.EntityFrameworkCore;
using HealthyHole.Models;

namespace HealthyHole.DAL
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=.\\DataBase.db");
        }
    }
}

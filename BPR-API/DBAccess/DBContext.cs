using BPR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BPR_API.DBAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Password> Passwords { get; set; }
        // public DbSet<Programme> Programmes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DB.db");
        }
    }
}

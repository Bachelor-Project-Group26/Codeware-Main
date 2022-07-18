using BPR_API.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BPR_API.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        //public DbSet<UserChat> UserChats { get; set; }
        //public DbSet<Chat> Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = BPR-DB.db");
        }
    }
}

using BPR_API.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BPR_API.DBAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DB.db");
        }
    }
}

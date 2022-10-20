using BPR_API.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BPR_API.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Following> FollowingList { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChat>().HasKey(c => new { c.UserId, c.ChatId });
            modelBuilder.Entity<Following>().HasKey(c => new { c.UserId, c.FollowedId });
            modelBuilder.Entity<Reaction>().HasKey(c => new { c.UserId, c.PostId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ./BPR-DB.db");
        }
    }
}

using BPR_API.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BPR_API.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserPassword> UserPasswords { get; set; }
        public virtual DbSet<UserChat> UserChats { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectOwner> SubjectOwners { get; set; }
        public virtual DbSet<Following> FollowingList { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChat>().HasKey(c => new { c.UserId, c.ChatId });
            modelBuilder.Entity<SubjectOwner>().HasKey(c => new { c.UserId, c.SubjectId });
            modelBuilder.Entity<Following>().HasKey(c => new { c.UserId, c.FollowedId });
            modelBuilder.Entity<Reaction>().HasKey(c => new { c.UserId, c.PostId });
            modelBuilder.Entity<Comment>().HasKey(c => new { c.CommentId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ./BPR-DB.db");
        }
    }
}

using FeedbackApp.Entities;
using FeedbackApp.Models.StoredFunctionsAndProcedures;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Data
{
    public class FeedbackAppDbContext : DbContext
    {
        public FeedbackAppDbContext(DbContextOptions<FeedbackAppDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserWithProjectDto> UsersWithProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Project)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Feedback)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FeedbackId);

            modelBuilder.Entity<UserWithProjectDto>().HasNoKey();
        }
    }
}

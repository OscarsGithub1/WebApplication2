using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Models.WorkTaskModel;

namespace WebApplication1.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
        public DbSet<UserWorkTask> UserWorkTasks { get; set; }
        public DbSet<WorkTaskHours> WorkTaskHours { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Role many-to-many relationship
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Seed Roles table with initial roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 3, Name = "Admin" },
                new Role { Id = 4, Name = "Ekonomi" }
            );

            // Configure User-WorkTask many-to-many relationship
            modelBuilder.Entity<UserWorkTask>()
                .HasKey(uwt => new { uwt.UserId, uwt.WorkTaskId });

            modelBuilder.Entity<UserWorkTask>()
                .HasOne(uwt => uwt.User)
                .WithMany(u => u.UserWorkTasks)
                .HasForeignKey(uwt => uwt.UserId);

            modelBuilder.Entity<UserWorkTask>()
                .HasOne(uwt => uwt.WorkTask)
                .WithMany(wt => wt.UserWorkTasks)
                .HasForeignKey(uwt => uwt.WorkTaskId);

            // Configure WorkTaskHours relationship
            modelBuilder.Entity<WorkTaskHours>()
                .HasOne(wth => wth.WorkTask)
                .WithMany(wt => wt.WorkTaskHours) // Ensure WorkTask model has ICollection<WorkTaskHours>
                .HasForeignKey(wth => wth.WorkTaskId);

            modelBuilder.Entity<WorkTaskHours>()
                .HasOne(wth => wth.User)
                .WithMany(u => u.WorkTaskHours) // Ensure User model has ICollection<WorkTaskHours>
                .HasForeignKey(wth => wth.UserId);
        }
    }
}

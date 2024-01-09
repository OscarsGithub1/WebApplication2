using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Models.BusinessOpportunitys;
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
        public DbSet<BusinessOpportunity> BusinessOpportunities { get; set; }
        public DbSet<UserBusinessOpportunity> UserBusinessOpportunities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User-Role Relationship Configuration
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

            // User-WorkTask Relationship Configuration
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

            // Configuration for WorkTask entity
            modelBuilder.Entity<WorkTask>()
                .Property(wt => wt.IsCompleted)
                .HasDefaultValue(false); // By default, a WorkTask is not completed

            // User-BusinessOpportunity Relationship Configuration
            modelBuilder.Entity<UserBusinessOpportunity>()
                .HasKey(ubo => new { ubo.UserId, ubo.BusinessOpportunityId });
            modelBuilder.Entity<UserBusinessOpportunity>()
                .HasOne(ubo => ubo.User)
                .WithMany(u => u.UserBusinessOpportunities)
                .HasForeignKey(ubo => ubo.UserId);
            modelBuilder.Entity<UserBusinessOpportunity>()
                .HasOne(ubo => ubo.BusinessOpportunity)
                .WithMany(bo => bo.UserBusinessOpportunities)
                .HasForeignKey(ubo => ubo.BusinessOpportunityId);

            // Configuration for Company entity
            modelBuilder.Entity<Company>()
                .HasOne(c => c.Responsible)
                .WithMany() // Assuming a User can be responsible for multiple companies
                .HasForeignKey("ResponsibleUserId") // EF Core will use this as the FK
                .OnDelete(DeleteBehavior.Restrict); // or another delete behavior as needed

            // Configuration for Customer entity
            modelBuilder.Entity<Customer>()
                .Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100); // Adjust the maximum length as needed

            modelBuilder.Entity<Customer>()
                .Property(c => c.Number)
                .HasMaxLength(50); // Adjust the maximum length as needed

            modelBuilder.Entity<Customer>()
                .Property(c => c.WebsiteLink)
                .HasMaxLength(200); // Adjust the maximum length as needed

            modelBuilder.Entity<Customer>()
                .Property(c => c.PostalCode)
                .HasMaxLength(20); // Adjust the maximum length as needed

            modelBuilder.Entity<Customer>()
                .Property(c => c.TypeOfCustomer)
                .HasMaxLength(100); // Adjust the maximum length as needed

            // Add configurations for other entities and their properties as needed
        }
    }
}

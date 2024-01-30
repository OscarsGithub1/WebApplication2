using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Models.BusinessOpportunitys;
using WebApplication1.Models.DTO;
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
        public DbSet<WorkTaskOpportunity> WorkTaskOpportunities { get; set; } // Add DbSet for WorkTaskOpportunity
        public DbSet<UserWorkTaskOpportunity> UserWorkTaskOpportunities { get; set; } // Add DbSet for UserWorkTaskOpportunity

        // Update DbSet for BusinessOpportunities to use HashSet
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

            // Add specific configurations for new WorkTask properties
            modelBuilder.Entity<WorkTask>()
                .Property(wt => wt.Totalvärde)
                .HasDefaultValue(0); // Default value for Totalvärde
            modelBuilder.Entity<WorkTask>()
                .Property(wt => wt.AvtalAnsvarig)
                .HasMaxLength(100); // Maximum length for AvtalAnsvarig
            modelBuilder.Entity<WorkTask>()
                .Property(wt => wt.AvtalKontakt)
                .HasMaxLength(100); // Maximum length for AvtalKontakt

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

            // Configuration for WorkTaskOpportunity entity
            modelBuilder.Entity<WorkTaskOpportunity>()
                .Property(wo => wo.Company)
                .IsRequired()
                .HasMaxLength(100); // Adjust the maximum length as needed
            modelBuilder.Entity<WorkTaskOpportunity>()
                .Property(wo => wo.Description)
                .HasMaxLength(500); // Adjust the maximum length as needed
            modelBuilder.Entity<WorkTaskOpportunity>()
                .Property(wo => wo.OpportunityDate)
                .IsRequired();
            modelBuilder.Entity<WorkTaskOpportunity>()
                .Property(wo => wo.OpportunityValue)
                .IsRequired()
                .HasColumnType("decimal(18, 2)"); // Specify the column type for OpportunityValue
            modelBuilder.Entity<WorkTaskOpportunity>()
                .Property(wo => wo.IsClosed)
                .HasDefaultValue(false); // Default value for IsClosed

            // Configuration for User-WorkTaskOpportunity Relationship
            modelBuilder.Entity<UserWorkTaskOpportunity>()
                .HasKey(ow => new { ow.UserId, ow.WorkTaskOpportunityId });
            modelBuilder.Entity<UserWorkTaskOpportunity>()
                .HasOne(ow => ow.User)
                .WithMany(u => u.UserWorkTaskOpportunities)
                .HasForeignKey(ow => ow.UserId);
            modelBuilder.Entity<UserWorkTaskOpportunity>()
                .HasOne(ow => ow.WorkTaskOpportunity)
                .WithMany(wo => wo.UserWorkTaskOpportunities)
                .HasForeignKey(ow => ow.WorkTaskOpportunityId);

            // Add configurations for other entities and their properties as needed
        }
    }
}

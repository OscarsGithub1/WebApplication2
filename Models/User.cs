using WebApplication1.Models.WorkTaskModel;
using WebApplication1.Models;

public class User
{
    public User()
    {
        UserRoles = new HashSet<UserRole>();
        UserWorkTasks = new HashSet<UserWorkTask>();
        WorkTaskHours = new HashSet<WorkTaskHours>();
        UserBusinessOpportunities = new HashSet<UserBusinessOpportunity>();
    }

    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // Additional properties
    public DateTime LastLogin { get; set; }
    public int LoginCount { get; set; }

    // Collections
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<UserWorkTask> UserWorkTasks { get; set; }
    public ICollection<WorkTaskHours> WorkTaskHours { get; set; }
    public ICollection<UserBusinessOpportunity> UserBusinessOpportunities { get; set; }
    public ICollection<UserWorkTaskOpportunity> UserWorkTaskOpportunities { get; set; }

}

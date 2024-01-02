using WebApplication1.Models.WorkTaskModel;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Additional properties for endpoint-related information
        public DateTime LastLogin { get; set; }
        public int LoginCount { get; set; }

        //
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserWorkTask> UserWorkTasks { get; set; }
        public ICollection<WorkTaskHours> WorkTaskHours { get; set; }


    }
}

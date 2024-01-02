namespace WebApplication1.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for the relationship
        public ICollection<UserRole> UserRoles { get; set; }
    }
}

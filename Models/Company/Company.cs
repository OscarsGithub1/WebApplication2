namespace WebApplication1.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public User Responsible { get; set; } // Navigation property for User
        public string PostalCity { get; set; }
        public string OrganizationNumber { get; set; }
    }
}

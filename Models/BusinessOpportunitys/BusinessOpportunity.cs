namespace WebApplication1.Models.BusinessOpportunitys
{
    public class BusinessOpportunity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PotentialStartDate { get; set; }
        public string Status { get; set; }

        // ... other properties ...

        public ICollection<UserBusinessOpportunity> UserBusinessOpportunities { get; set; }
    }
}

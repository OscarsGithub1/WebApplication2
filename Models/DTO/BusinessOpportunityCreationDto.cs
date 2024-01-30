namespace WebApplication1.Models.DTO
{
    public class BusinessOpportunityCreationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PotentialStartDate { get; set; }
        public string Status { get; set; }
        public List<int> UserIds { get; set; }
        public int UserId { get; set; } // Add this line
    }
}


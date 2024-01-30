namespace WebApplication1.Models.DTO
{
    public class BusinessOpportunityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PotentialStartDate { get; set; }
        public string Status { get; set; }

        // Include other properties as needed

        // Example: Include user information
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}

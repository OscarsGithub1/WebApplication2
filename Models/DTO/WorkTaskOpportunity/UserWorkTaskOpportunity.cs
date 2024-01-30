using System;

namespace WebApplication1.Models.WorkTaskModel
{
    public class UserWorkTaskOpportunity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int WorkTaskOpportunityId { get; set; }
        public WorkTaskOpportunity WorkTaskOpportunity { get; set; }

        // Add any other properties or navigation properties as needed
    }
}

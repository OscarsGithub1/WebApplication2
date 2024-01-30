namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskCreationDto
    {
        public string Company { get; set; }
        public string ContactPerson { get; set; }
        public DateTime OrderDate { get; set; }
        public string Currency { get; set; }
        public string PipelineLevel { get; set; }
        public string SalesResponsible { get; set; }
        public string Description { get; set; }
        public double ScopePercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Role { get; set; }
        public string AssignmentDescription { get; set; }
        public string CompetenceRequirements { get; set; }
        public string OtherRequirements { get; set; }
        public string Placement { get; set; }

        public decimal PricePerHour { get; set; }
        public decimal Totalvärde { get; set; } // Newly added property
        public string AvtalAnsvarig { get; set; } // Newly added property
        public string AvtalKontakt { get; set; } // Newly added property
        public List<int> UserIds { get; set; } // List of User IDs to be associated with the WorkTask

        // Add any other fields that are part of the form
    }
}

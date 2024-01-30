namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskOpportunityCreationDto
    {
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime OpportunityDate { get; set; }
        public decimal OpportunityValue { get; set; }
        public bool IsClosed { get; set; }
        public List<int> UserIds { get; set; } // List of User IDs to be associated with the WorkTaskOpportunity

        public string PipelineLevel { get; set; }
        public string SalesResponsible { get; set; }

        // Add any other fields that are part of the form
    }
}

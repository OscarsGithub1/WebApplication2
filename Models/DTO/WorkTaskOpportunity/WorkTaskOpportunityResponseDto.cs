namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskOpportunityResponseDto
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime OpportunityDate { get; set; }
        public decimal OpportunityValue { get; set; }
        public bool IsClosed { get; set; }
        public string SalesResponsible { get; set; } // Add SalesResponsible property
        public string PipelineLevel { get; set; } 
        // Add any other properties that you want to include in the response
    }
}

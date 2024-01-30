namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskDto
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public int WorkTaskId { get; set; }
        public double HoursWorked { get; set; }
        public string ContactPerson { get; set; } // Existing line
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
        public decimal Totalvärde { get; set; }
        public string AvtalAnsvarig { get; set; }
        public string AvtalKontakt { get; set; }
        public bool IsCompleted { get; set; }
     

        // Add other fields as needed
    }
}

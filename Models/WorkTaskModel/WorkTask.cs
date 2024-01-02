namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTask
    {
        public int Id { get; set; }
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
        public double HoursWorked { get; set; } // Add this line


        public ICollection<UserWorkTask> UserWorkTasks { get; set; }

        public ICollection<WorkTaskHours> WorkTaskHours { get; set; }

        // Add any other fields as necessary
    }

}

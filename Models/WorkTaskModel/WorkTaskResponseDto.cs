namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskResponseDto
    {
        // Include only the properties you want to return
        public int Id { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public decimal Totalvärde { get; set; } // Matches Totalvärde from Deals
        public DateTime OrderDate { get; set; }

        public string AvtalAnsvarig { get; set; }
        public string AvtalKontakt { get; set; }
        // ... other properties but exclude the UserWorkTasks collection
    }
}

namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskDto
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public int WorkTaskId { get; set; }
        public double HoursWorked { get; set; }
        public string ContactPerson { get; set; } // Add this line
        public DateTime OrderDate { get; set; }



    }
}

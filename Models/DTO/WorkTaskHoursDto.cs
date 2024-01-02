namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskHoursDto
    {
        public int WorkTaskId { get; set; }
        public int UserId { get; set; } // Add this if needed
        public double HoursWorked { get; set; }
    }
}

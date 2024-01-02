namespace WebApplication1.Models
{
    public class WorkTaskReceiptDto
    {
        public string UserName { get; set; }
        public string Company { get; set; }
        public double HoursWorked { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal TotalAmount => (decimal)HoursWorked * PricePerHour;
    }
}

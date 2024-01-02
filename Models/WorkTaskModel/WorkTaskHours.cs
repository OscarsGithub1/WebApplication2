using WebApplication1.Models.WorkTaskModel;
using WebApplication1.Models;

public class WorkTaskHours
{
    public int Id { get; set; }
    public int WorkTaskId { get; set; }
    public WorkTask WorkTask { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public double Hours { get; set; }
    public DateTime DateLogged { get; set; } // Optional, for tracking when the hours were logged
}

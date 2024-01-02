namespace WebApplication1.Models.WorkTaskModel
{
    public class UserWorkTask
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int WorkTaskId { get; set; }
        public WorkTask WorkTask { get; set; }
    }

}

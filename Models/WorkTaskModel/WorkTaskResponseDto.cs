namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskResponseDto
    {
        // Include only the properties you want to return
        public int Id { get; set; }
        public string Company { get; set; }
        // ... other properties but exclude the UserWorkTasks collection
    }
}

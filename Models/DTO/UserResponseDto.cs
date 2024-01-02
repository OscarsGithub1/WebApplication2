namespace WebApplication1.Models.DTO
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        // Add other properties you want to return in the response
        // Do NOT include navigation properties like UserRoles
    }
}

namespace WebApplication1.Models.DTO
{
    public class LoginResponse
    {
        public UserResponseDto User { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}

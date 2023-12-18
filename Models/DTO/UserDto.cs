using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
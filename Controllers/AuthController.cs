using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            // Check if the username already exists
            if (users.Exists(u => u.UserName == request.Username))
            {
                return BadRequest("Username already exists");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User newUser = new User
            {
                UserName = request.Username,
                PasswordHash = passwordHash
            };

            users.Add(newUser);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            User user = users.Find(u => u.UserName == request.Username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Generate a random key (64 bytes = 512 bits)
            var randomBytes = new byte[64];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var key = new SymmetricSecurityKey(randomBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

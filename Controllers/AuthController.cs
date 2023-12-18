using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDataContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDataContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            // Check if the username already exists in the database
            if (await _dbContext.Users.AnyAsync(u => u.UserName == request.Username))
            {
                return BadRequest("Username already exists");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User newUser = new User
            {
                UserName = request.Username,
                PasswordHash = passwordHash
            };

            // Add the new user to the database
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.Username);

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

            var keyBytes = Convert.FromBase64String(_configuration.GetSection("AppSettings:Token").Value);

            // Ensure key size is at least 512 bits
            if (keyBytes.Length < 64)
            {
                throw new InvalidOperationException("Key size is too small for the chosen algorithm.");
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

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

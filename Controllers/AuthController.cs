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
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<UserResponseDto>> Register(UserDto request)
        {
            // Check if the username already exists
            if (await _dbContext.Users.AnyAsync(u => u.UserName == request.Username))
            {
                return BadRequest("Username already exists");
            }

            // Create new user
            var newUser = new User
            {
                UserName = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
                // ... other user properties as needed ...
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            // Assign the "Consult" role to the user
            var consultRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Consult");
            if (consultRole == null)
            {
                // Handle the case where the "Consult" role does not exist
                return BadRequest("Default role not found.");
            }

            var newUserRole = new UserRole
            {
                UserId = newUser.Id,
                RoleId = consultRole.Id
            };

            _dbContext.UserRoles.Add(newUserRole);
            await _dbContext.SaveChangesAsync();

            // Prepare the response
            var response = new UserResponseDto
            {
                Id = newUser.Id,
                Username = newUser.UserName
                // ... map other required properties ...
            };

            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(UserDto request)
        {
            var user = await _dbContext.Users
                                       .Include(u => u.UserRoles)
                                           .ThenInclude(ur => ur.Role)
                                       .FirstOrDefaultAsync(u => u.UserName == request.Username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password");
            }

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
            string token = CreateToken(user);

            var response = new LoginResponse
            {
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.UserName
                    // ... map other required properties ...
                },
                Token = token,
                Roles = roles
            };

            return Ok(response);
        }

        [Authorize(Roles = "Admin")] // Only allow access to admins
        [HttpPost("changeRole")]
        public async Task<IActionResult> ChangeUserRole(ChangeRoleDto changeRoleDto)
        {
            var user = await _dbContext.Users
                                       .Include(u => u.UserRoles)
                                       .FirstOrDefaultAsync(u => u.Id == changeRoleDto.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var newRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == changeRoleDto.NewRoleName);
            if (newRole == null)
            {
                return NotFound("Role not found.");
            }

            // Remove existing roles
            _dbContext.UserRoles.RemoveRange(user.UserRoles);

            // Assign new role
            user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = newRole.Id });
            await _dbContext.SaveChangesAsync();

            return Ok("User role updated successfully.");
        }


        [HttpPost("createAdmin")]
        public async Task<IActionResult> CreateAdminUser()
        {
            var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (adminRole == null)
            {
                return BadRequest("Admin role not found.");
            }

            var adminUser = new User
            {
                UserName = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminPassword")
                // Set other required user properties...
            };

            _dbContext.Users.Add(adminUser);
            await _dbContext.SaveChangesAsync();

            _dbContext.UserRoles.Add(new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id });
            await _dbContext.SaveChangesAsync();

            return Ok("Admin user created successfully.");
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

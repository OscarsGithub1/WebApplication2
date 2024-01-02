using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.WorkTaskModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkTaskController : ControllerBase
    {
        private readonly AppDataContext _context;

        public WorkTaskController(AppDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkTask([FromBody] WorkTaskCreationDto workTaskDto)
        {
            // Creating a new WorkTask from the DTO
            var workTask = new WorkTask
            {
                Company = workTaskDto.Company,
                ContactPerson = workTaskDto.ContactPerson,
                OrderDate = workTaskDto.OrderDate,
                Currency = workTaskDto.Currency,
                PipelineLevel = workTaskDto.PipelineLevel,
                SalesResponsible = workTaskDto.SalesResponsible,
                Description = workTaskDto.Description,
                ScopePercentage = workTaskDto.ScopePercentage,
                StartDate = workTaskDto.StartDate,
                EndDate = workTaskDto.EndDate,
                DueDate = workTaskDto.DueDate,
                Role = workTaskDto.Role,
                AssignmentDescription = workTaskDto.AssignmentDescription,
                CompetenceRequirements = workTaskDto.CompetenceRequirements,
                OtherRequirements = workTaskDto.OtherRequirements,
                Placement = workTaskDto.Placement,
                PricePerHour = workTaskDto.PricePerHour,


            };

            // Adding the WorkTask to the DbContext
            _context.WorkTasks.Add(workTask);
            await _context.SaveChangesAsync(); // Save to get the WorkTask Id

            // Associating users with the WorkTask
            if (workTaskDto.UserIds != null && workTaskDto.UserIds.Count > 0)
            {
                foreach (var userId in workTaskDto.UserIds)
                {
                    var userWorkTask = new UserWorkTask
                    {
                        UserId = userId,
                        WorkTaskId = workTask.Id
                    };
                    _context.UserWorkTasks.Add(userWorkTask);
                }
                await _context.SaveChangesAsync(); // Save the UserWorkTask relationships
            }

            // Creating a DTO for response to avoid serialization issues
            var responseDto = new WorkTaskResponseDto
            {
                Id = workTask.Id,
                Company = workTask.Company,
                // ... Map other properties as needed
            };

            // Returning the created WorkTask DTO
            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkTasks()
        {
            var workTasks = await _context.WorkTasks
                .Select(wt => new WorkTaskDto
                {
                    Id = wt.Id,
                    Company = wt.Company,
                    // Map other properties
                })
                .ToListAsync();

            return Ok(workTasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkTask(int id, [FromBody] WorkTaskUpdateDto updateDto)
        {
            var workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }

            // Update properties
            workTask.Company = updateDto.Company;
            // Update other properties as necessary

            _context.WorkTasks.Update(workTask);
            await _context.SaveChangesAsync();

            return NoContent(); // or return Ok(workTask) if you want to return the updated task
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTask(int id)
        {
            var workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }

            _context.WorkTasks.Remove(workTask);
            await _context.SaveChangesAsync();

            return NoContent(); // Standard response for a successful DELETE request


        }

        [HttpPost("reportHours")]
        public async Task<IActionResult> ReportHours([FromBody] WorkTaskHoursDto hoursDto)
        {
            var workTask = await _context.WorkTasks.FindAsync(hoursDto.WorkTaskId);
            if (workTask == null)
            {
                return NotFound("WorkTask not found.");
            }

            // Here you can add additional checks, like whether the user is assigned to this WorkTask

            workTask.HoursWorked = hoursDto.HoursWorked;
            await _context.SaveChangesAsync();

            return Ok("Hours reported successfully.");
        }

        [HttpGet("generateReceipt/{userId}")]
        public async Task<IActionResult> GenerateReceipt(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var userWorkTasks = await _context.UserWorkTasks
                .Include(uwt => uwt.WorkTask)
                .Where(uwt => uwt.UserId == userId)
                .ToListAsync();

            var receipt = userWorkTasks.Select(uwt => new WorkTaskReceiptDto
            {
                UserName = user.UserName,
                Company = uwt.WorkTask.Company,
                HoursWorked = uwt.WorkTask.HoursWorked,
                PricePerHour = uwt.WorkTask.PricePerHour
            }).ToList();

            return Ok(receipt);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWorkTasks(int userId)
        {
            var userWorkTasks = await _context.UserWorkTasks
                .Where(uwt => uwt.UserId == userId)
                .Select(uwt => uwt.WorkTask)
                .Select(wt => new WorkTaskDto
                {
                    Id = wt.Id,
                    Company = wt.Company,
                    ContactPerson = wt.ContactPerson,
                    OrderDate = wt.OrderDate,
                    // ... other properties as needed
                })
                .ToListAsync();

            if (!userWorkTasks.Any())
            {
                return NotFound("No work tasks found for the specified user.");
            }

            return Ok(userWorkTasks);
        }




    }
}

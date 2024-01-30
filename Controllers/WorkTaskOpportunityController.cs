using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.WorkTaskModel;
using WebApplication1.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Models.WorkTaskModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("worktaskopportunity")]
    public class WorkTaskOpportunityController : ControllerBase
    {
        private readonly AppDataContext _context;

        public WorkTaskOpportunityController(AppDataContext context)
        {
            _context = context;
        }

        [HttpPost("createWorkTaskOpportunity")]
        public async Task<IActionResult> CreateWorkTaskOpportunity([FromBody] WorkTaskOpportunityCreationDto workTaskOpportunityDto)
        {
            // Creating a new WorkTaskOpportunity from the DTO
            var workTaskOpportunity = new WorkTaskOpportunity
            {
                Company = workTaskOpportunityDto.Company,
                Description = workTaskOpportunityDto.Description,
                OpportunityDate = workTaskOpportunityDto.OpportunityDate,
                OpportunityValue = workTaskOpportunityDto.OpportunityValue,
                IsClosed = workTaskOpportunityDto.IsClosed,
                SalesResponsible = workTaskOpportunityDto.SalesResponsible,
                PipelineLevel = workTaskOpportunityDto.PipelineLevel
               
                // Add any other properties as needed
            };

            // Adding the WorkTaskOpportunity to the DbContext
            _context.WorkTaskOpportunities.Add(workTaskOpportunity);
            await _context.SaveChangesAsync(); // Save to get the WorkTaskOpportunity Id

            // Associating users with the WorkTaskOpportunity
            if (workTaskOpportunityDto.UserIds != null && workTaskOpportunityDto.UserIds.Count > 0)
            {
                foreach (var userId in workTaskOpportunityDto.UserIds)
                {
                    var userWorkTaskOpportunity = new UserWorkTaskOpportunity
                    {
                        UserId = userId,
                        WorkTaskOpportunityId = workTaskOpportunity.Id
                    };
                    _context.UserWorkTaskOpportunities.Add(userWorkTaskOpportunity);
                }
                await _context.SaveChangesAsync(); // Save the UserWorkTaskOpportunity relationships
            }

            // Creating a DTO for response to avoid serialization issues
            var responseDto = new WorkTaskOpportunityResponseDto
            {
                Id = workTaskOpportunity.Id,
                Company = workTaskOpportunity.Company,
                Description = workTaskOpportunity.Description,
                OpportunityDate = workTaskOpportunity.OpportunityDate,
                OpportunityValue = workTaskOpportunity.OpportunityValue,
                IsClosed = workTaskOpportunity.IsClosed,
                SalesResponsible = workTaskOpportunity.SalesResponsible,
                PipelineLevel = workTaskOpportunity.PipelineLevel,
                
               
                // ... Map other properties as needed for the response
            };

            // Returning the created WorkTaskOpportunity DTO
            return Ok(responseDto);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<WorkTaskOpportunity>>> GetUserWorkTaskOpportunities(int userId)
        {
            // Retrieve the user's work task opportunities using Include to include related data
            var userWorkTaskOpportunities = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.UserWorkTaskOpportunities)
                .Select(ow => ow.WorkTaskOpportunity)
                .ToListAsync();

            if (userWorkTaskOpportunities == null || !userWorkTaskOpportunities.Any())
            {
                return NotFound("No work task opportunities found for this user.");
            }

            return Ok(userWorkTaskOpportunities);
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkTaskOpportunity>> GetAllWorkTaskOpportunities()
        {
            var workTaskOpportunities = _context.WorkTaskOpportunities.ToList();
            return Ok(workTaskOpportunities);
        }

    }
}


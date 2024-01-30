using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BusinessOpportunitys;
using WebApplication1.Models.DTO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
public class BusinessOpportunitiesController : ControllerBase
{
    private readonly AppDataContext _context;

    public BusinessOpportunitiesController(AppDataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBusinessOpportunity([FromBody] BusinessOpportunityCreationDto dto)
    {
        // Retrieve the current user's ID
        int currentUserId = int.Parse(User.FindFirst("sub").Value);

        var businessOpportunity = new BusinessOpportunity
        {
            // Map properties from DTO to BusinessOpportunity
            Title = dto.Title,
            Description = dto.Description,
            PotentialStartDate = dto.PotentialStartDate,
            Status = dto.Status,

            // ... map other properties as needed
        };

        // Create a HashSet from the List of UserBusinessOpportunity
        var userBusinessOpportunities = new HashSet<UserBusinessOpportunity>
    {
        new UserBusinessOpportunity
        {
            UserId = dto.UserId,
            BusinessOpportunity = businessOpportunity
        }
    };

        // Assign the HashSet to the UserBusinessOpportunities property
        businessOpportunity.UserBusinessOpportunities = userBusinessOpportunities;

        _context.BusinessOpportunities.Add(businessOpportunity);
        await _context.SaveChangesAsync();

        return Ok(businessOpportunity);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessOpportunityDto>> GetBusinessOpportunityById(int id)
    {
        var businessOpportunity = await _context.BusinessOpportunities.FindAsync(id);

        if (businessOpportunity == null)
        {
            return NotFound();
        }

        var responseDto = new BusinessOpportunityDto
        {
            Id = businessOpportunity.Id,
            Title = businessOpportunity.Title,
            Description = businessOpportunity.Description,
            PotentialStartDate = businessOpportunity.PotentialStartDate,
            Status = businessOpportunity.Status
            // Map other properties to DTO as needed
        };

        return Ok(responseDto);
    }


    // Other controller methods...
}

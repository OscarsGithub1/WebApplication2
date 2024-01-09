using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BusinessOpportunitys;
using System.Threading.Tasks;
using WebApplication1.Models.BusinessOpportunitys;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessOpportunityController : ControllerBase
    {
        private readonly AppDataContext _context;

        public BusinessOpportunityController(AppDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusinessOpportunity([FromBody] BusinessOpportunityCreationDto dto)
        {
            var businessOpportunity = new BusinessOpportunity
            {
                // Map properties from DTO to BusinessOpportunity
                Title = dto.Title,
                Description = dto.Description,
                PotentialStartDate = dto.PotentialStartDate,
                Status = dto.Status,

                // ... map other properties as needed


            };

            _context.BusinessOpportunities.Add(businessOpportunity);
            await _context.SaveChangesAsync();

            return Ok(businessOpportunity); // or return CreatedAtAction(...) if you want to return a specific action result
        }
    }
}

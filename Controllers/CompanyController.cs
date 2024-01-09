using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Threading.Tasks;
using WebApplication1.Models.DTO.CompanyDTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public CompaniesController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _context.Companies.ToListAsync();
            return Ok(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // POST: api/Companies


        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto companyDto)
        {
            // Check if the responsible user exists
            var responsibleUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == companyDto.ResponsibleUserId);
            if (responsibleUser == null)
            {
                return BadRequest("Responsible user not found.");
            }

            var company = new Company
            {
                Name = companyDto.Name,
                Status = companyDto.Status,
                Type = companyDto.Type,
                Responsible = responsibleUser, // Assign the User object directly
                PostalCity = companyDto.PostalCity,
                OrganizationNumber = companyDto.OrganizationNumber
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return Ok(company); // Or return CreatedAtAction(...)
        }

        // Other actions (PUT, DELETE) as needed
    }
}

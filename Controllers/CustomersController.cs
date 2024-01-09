using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication1.Models.DTO.CustomerDTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDataContext _context;

        public CustomersController(AppDataContext context)
        {
            _context = context;
        }

        // POST api/Customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreationDto customerDto)
        {
            var customer = new Customer
            {
                CompanyName = customerDto.CompanyName,
                Number = customerDto.Number,
                WebsiteLink = customerDto.WebsiteLink,
                PostalCode = customerDto.PostalCode,
                TypeOfCustomer = customerDto.TypeOfCustomer
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(customer); // Or return CreatedAtAction(...)
        }

        // GET api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerDto
                {
                    CompanyName = c.CompanyName,
                    Number = c.Number,
                    WebsiteLink = c.WebsiteLink,
                    PostalCode = c.PostalCode,
                    TypeOfCustomer = c.TypeOfCustomer
                })
                .ToListAsync();

            return Ok(customers);
        }
    }
}
    
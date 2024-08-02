using EmployeePortal.Application.DTO.Department;
using EmployeePortal.DataAccess.Common;
using EmployeePortal.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepartmentPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var customers = await _dbContext.Customer.ToListAsync();

            return Ok(customers);
        }

        [HttpGet]
        [Route("ByCustomerId")]
        public async Task<ActionResult> Get(int customerId)
        {
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(x => x.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}

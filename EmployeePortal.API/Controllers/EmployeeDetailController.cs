using EmployeePortal.Application.DTO.Employee;
using EmployeePortal.DataAccess.Common;
using EmployeePortal.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailDetailController : ControllerBase
    {
        ApplicationDbContext _dbContext;
        public EmployeeDetailDetailController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var EmployeeDetailList = await _dbContext.EmployeeDetail.ToListAsync();

            return Ok(EmployeeDetailList);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int EmployeeDetailId)
        {
            var EmployeeDetailList = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x=> x.Id ==EmployeeDetailId);

            if(EmployeeDetailList == null)
            {
                return NotFound("Invalid Id");
            }

            return Ok(EmployeeDetailList);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Creaet(CreateEmployeeDetailDto employeeDetailDto)
        {
           EmployeeDetail employeeDetail = new EmployeeDetail
            {
               EmployeeId = employeeDetailDto.EmployeeId,
               Age = employeeDetailDto.Age,
               Gender = employeeDetailDto.Gender,
               PhoneNo = employeeDetailDto.PhoneNo,
               EmailAddress = employeeDetailDto.EmailAddress,
               Address = employeeDetailDto.Address
           };

            _dbContext.EmployeeDetail.Add(employeeDetail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Update(UpdateEmployeeDetailDto employeeDetailDto)
        {
            var employeeFromdB = _dbContext.EmployeeDetail.AsNoTracking().Where(x => x.Id ==employeeDetailDto.Id).FirstOrDefault();

            if (employeeFromdB == null)
            {
                return NotFound();
            }

           EmployeeDetail employee = new EmployeeDetail
            {
               Id = employeeDetailDto.Id,
               EmployeeId = employeeDetailDto.EmployeeId,
               Age = employeeDetailDto.Age,
               Gender = employeeDetailDto.Gender,
               PhoneNo = employeeDetailDto.PhoneNo,
               EmailAddress = employeeDetailDto.EmailAddress,
               Address = employeeDetailDto.Address

           };

            _dbContext.EmployeeDetail.Update(employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int employeeDetailId)
        {
            var EmployeeDetail = _dbContext.EmployeeDetail.Where(x => x.Id == employeeDetailId).FirstOrDefault();

            if (EmployeeDetail == null)
            {
                return NotFound();
            }

            _dbContext.EmployeeDetail.Remove(EmployeeDetail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

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
    public class DepartmentController : ControllerBase
    {
        ApplicationDbContext _dbContext;
        public DepartmentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var DepartmentList = await _dbContext.Department.ToListAsync();

            return Ok(DepartmentList);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int DepartmentId)
        {
            var DepartmentList = await _dbContext.Department.FirstOrDefaultAsync(x=> x.Id == DepartmentId);

            if(DepartmentList == null)
            {
                return NotFound("Invalid Id");
            }

            return Ok(DepartmentList);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Creaet(CreateDepartmentDto DepartmentDto)
        {
            Department department = new Department
            {
                Name = DepartmentDto.Name
            };

            _dbContext.Department.Add(department);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Update(UpdateDepartmentDto DepartmentDto)
        {
            var DepartmentFromdB = _dbContext.Department.AsNoTracking().Where(x => x.Id == DepartmentDto.Id).FirstOrDefault();

            if (DepartmentFromdB == null)
            {
                return NotFound();
            }

            Department Department = new Department
            {
                Id = DepartmentDto.Id,
                Name = DepartmentDto.Name
            };

            _dbContext.Department.Update(Department);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int dppartmentId)
        {
            var Department = _dbContext.Department.Where(x => x.Id == dppartmentId).FirstOrDefault();

            if (Department == null)
            {
                return NotFound();
            }

            _dbContext.Department.Remove(Department);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

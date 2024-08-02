using AutoMapper;
using EmployeePortal.Application.DTO.Employee;
using EmployeePortal.Application.ViewModels;
using EmployeePortal.DataAccess.Common;
using EmployeePortal.Domain.ApplicationEnums;
using EmployeePortal.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var EmployeeList = await _dbContext.Employee.ToListAsync();

            return Ok(EmployeeList);
        }

        [HttpGet]
        [Route("ListWithDetails")]
        public async Task<ActionResult> GetDetails()
        {
            var employees = await _dbContext.Employee.Include(x => x.Department)
                .Include(x => x.EmployeeDetail).ToListAsync();

            var empMapped = _mapper.Map<List<EmployeeDetailVM>>(employees);

            return Ok(empMapped);
        }


        [HttpGet]
        [Route("ByRawSql")]
        public ActionResult GetEmployees()
        {
            var employees = _dbContext.Employee.FromSqlRaw("select * from dbo.Employee where employedFrom ='2024'").ToList();

            return Ok(employees);
        }


        [HttpGet]
        [Route("EmployeeDataView")]
        public ActionResult GetEmployeeData()
        {
            var employees = _dbContext.EmployeeData.ToList();

            return Ok(employees);
        }

        [HttpGet]
        [Route("ByEmployeeIdSP")]
        public async Task<ActionResult> GetEmployeeByIDSP(int employeeId)
        {
            var employee = _dbContext.Employee.FromSqlRaw("EXEC GetEmployeeById @EmployeeId", new SqlParameter("@EmployeeId", employeeId));

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int EmployeeId)
        {
            var EmployeeList = await _dbContext.Employee.FirstOrDefaultAsync(x=> x.Id ==EmployeeId);

            if(EmployeeList == null)
            {
                return NotFound("Invalid Id");
            }

            return Ok(EmployeeList);
        }

        [HttpGet]
        [Route("FilterByYearAndDepartment")]
        public async Task<ActionResult> GetEmployeeByFilter(int year, int departmentId)
        {
            //var employee = await _dbContext.Employee.FirstAsync();

            //var employee = await _dbContext.Employee.FirstAsync(x=>x.EmployedFrom == year);

            //var employee = await _dbContext.Employee.FirstOrDefaultAsync(x => x.EmployedFrom == year);

            var employee = await _dbContext.Employee.Where(x => x.EmployedFrom == year && x.DepartmentId == departmentId).ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
        [Route("GetListBasedOnEmpolyedYear")]
        public async Task<ActionResult<Employee>> GetOrderedList(Order order)
        {
            List<Employee> employees = new List<Employee>();

            switch (order)
            {
                case Order.Asc:

                    employees = await _dbContext.Employee.OrderBy(x => x.EmployedFrom).ToListAsync();

                    break;

                case Order.Desc:

                    employees = await _dbContext.Employee.OrderByDescending(x => x.EmployedFrom).ToListAsync();

                    break;
            }

            return Ok(employees);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Creaet(CreateEmployeeDto employeeDto)
        {
            var result = _dbContext.Department.Any(x => x.Id == employeeDto.DepartmentId);

            if (!result)
            {
                return BadRequest("Invalid Department Id");
            }

            Employee employee = new Employee
            {
               FirstName = employeeDto.FirstName,
               MiddleName = employeeDto.MiddleName,
               LastName = employeeDto.LastName,
               EmployedFrom = employeeDto.EmployedFrom,
               DepartmentId = employeeDto.DepartmentId,
           };

            _dbContext.Employee.Add(employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Update(UpdateEmployeeDto employeeDto)
        {
            var employeeFromdB = _dbContext.Employee.AsNoTracking().Where(x => x.Id ==employeeDto.Id).FirstOrDefault();

            if (employeeFromdB == null)
            {
                return NotFound();
            }

           Employee employee = new Employee
            {
               FirstName = employeeFromdB.FirstName,
               MiddleName = employeeFromdB.MiddleName,
               LastName = employeeFromdB.LastName,
               EmployedFrom = employeeFromdB.EmployedFrom,
               DepartmentId = employeeFromdB.DepartmentId,

            };

            _dbContext.Employee.Update(employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            var Employee = _dbContext.Employee.Where(x => x.Id == employeeId).FirstOrDefault();

            if (Employee == null)
            {
                return NotFound();
            }

            _dbContext.Employee.Remove(Employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

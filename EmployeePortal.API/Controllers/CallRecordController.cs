
using AutoMapper;
using EmployeePortal.Application.DTO.CallRecords;
using EmployeePortal.Application.ViewModels;
using EmployeePortal.DataAccess.Common;
using EmployeePortal.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CallRecordPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallRecordController : ControllerBase
    {
        ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CallRecordController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var CallRecordList = await _dbContext.CallRecord.ToListAsync();

            return Ok(CallRecordList);
        }

        [HttpGet]
        [Route("GetDetails")]
        public async Task<IActionResult> GetDetails()
        {
            var callRecords = await _dbContext.CallRecord.Include(x => x.Employee).Include(x => x.Customer).ToListAsync();

            var calls = _mapper.Map<List<CallRecordVM>>(callRecords);

            return Ok(calls);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int CallRecordId)
        {
            var CallRecordList = await _dbContext.CallRecord.FirstOrDefaultAsync(x => x.Id == CallRecordId);

            if (CallRecordList == null)
            {
                return NotFound("Invalid Id");
            }

            return Ok(CallRecordList);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Creaet(CreateCallRecordDto callRecordDto)
        {
            CallRecord callRecord = new CallRecord
            {
                StartTime = callRecordDto.StartTime,
                EndTime = callRecordDto.EndTime,
                CallType = callRecordDto.CallType,
                EmployeeId = callRecordDto.EmployeeId,
                CustomerId = callRecordDto.CustomerId
            };

            _dbContext.CallRecord.Add(callRecord);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Update(UpdateCallRecordDto callRecordDto)
        {
            var callRecordFromdB = _dbContext.CallRecord.AsNoTracking().Where(x => x.Id == callRecordDto.Id).FirstOrDefault();

            if (callRecordFromdB == null)
            {
                return NotFound();
            }

            CallRecord callRecord = new CallRecord
            {
                Id = callRecordDto.Id,
                StartTime = callRecordDto.StartTime,
                EndTime = callRecordDto.EndTime,
                CallType = callRecordDto.CallType,
                EmployeeId = callRecordDto.EmployeeId,
                CustomerId = callRecordDto.CustomerId

            };

            _dbContext.CallRecord.Update(callRecord);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int callRecordId)
        {
            var CallRecord = _dbContext.CallRecord.Where(x => x.Id == callRecordId).FirstOrDefault();

            if (CallRecord == null)
            {
                return NotFound();
            }

            _dbContext.CallRecord.Remove(CallRecord);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

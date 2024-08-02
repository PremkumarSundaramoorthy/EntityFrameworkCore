using EmployeePortal.Domain.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Application.DTO.CallRecords
{
    public class UpdateCallRecordDto
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public CallType CallType { get; set; }

        public int EmployeeId { get; set; }

        public int CustomerId { get; set; }


    }
}

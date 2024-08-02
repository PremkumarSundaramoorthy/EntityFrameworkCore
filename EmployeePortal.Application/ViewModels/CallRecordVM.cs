using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Application.ViewModels
{
    public class CallRecordVM
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string CallType { get; set; }


        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }


        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}

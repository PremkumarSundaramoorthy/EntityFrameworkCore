using EmployeePortal.Domain.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Application.DTO.Employee
{
    public class UpdateEmployeeDetailDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public long PhoneNo { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }
    }
}

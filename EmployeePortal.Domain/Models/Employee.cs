using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Domain.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(2000, 2025, ErrorMessage = "Employed year should be between 1980 to 2100 only")]
        public int EmployedFrom { get; set; }

        public virtual EmployeeDetail EmployeeDetail { get; set; }

        public virtual ICollection<CallRecord> CallRecords { get; set; }

        public Employee()
        {
        }

        public Employee(int departmentId, string firstName, string middlename, string lastName, int employedFrom)
        {
            DepartmentId = departmentId;
            FirstName = firstName;
            MiddleName = middlename;
            LastName = lastName;
            EmployedFrom = employedFrom;
        }
    }
}

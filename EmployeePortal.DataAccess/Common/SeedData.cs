using EmployeePortal.Domain.ApplicationEnums;
using EmployeePortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.DataAccess.Common
{
    public class SeedData
    {
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext)
        {
            if (!_dbContext.Department.Any())
            {
                _dbContext.Department.AddRange(
                    new Department { Name = "HR" },
                    new Department { Name = "Sales" },
                    new Department { Name = "Marketing" },
                    new Department { Name = "R&D" }
                    );

                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Employee.Any())
            {

                _dbContext.Employee.AddRange(
                    new Employee(1, "Subramaniyam", "", "S", 2024),
                    new Employee(1, "Menaka", "", "Sekar", 2025),
                    new Employee(2, "Swetha", "", "Sri", 2023),
                    new Employee(2, "Riya", "", "Sri", 2023),
                    new Employee(3, "Devi", "Priya", "Murugan", 2024),
                    new Employee(3, "Vignesh", "", "Mani", 2022),
                    new Employee(3, "Saravanan", "", "Bala", 2024),
                    new Employee(4, "Karthik", "", "P", 2023),
                    new Employee(4, "Sanjay", "", "Krish", 2023),
                    new Employee(4, "Rithul", "", "V", 2023));

                await _dbContext.SaveChangesAsync();

            }

            if (!_dbContext.EmployeeDetail.Any())
            {

                _dbContext.EmployeeDetail.AddRange
                    (
                    new EmployeeDetail(1, 26, Gender.Male, 9486123453, "user1@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(2, 23, Gender.Male, 9486123453, "user2@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(3, 27, Gender.Male, 9486123453, "user3@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(4, 24, Gender.Female, 9486123453, "user4@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(5, 30, Gender.Female, 9486123453, "user5@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(6, 26, Gender.Female, 9486123453, "user6@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(7, 22, Gender.Female, 9486123453, "user7@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(8, 23, Gender.Female, 9486123453, "user8@gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(9, 28, Gender.Male, 9486123453, "user9gmail.com", "ABC Street,Chennai"),
                    new EmployeeDetail(10, 21, Gender.Male, 9486123453, "user10@gmail.com", "ABC Street,Chennai")
                    );

                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Customer.Any())
            {

                _dbContext.Customer.AddRange(
                    new Customer("Vikram"),
                    new Customer("Siddharth"),
                    new Customer("Priya"),
                    new Customer("Aishwarya"),
                    new Customer("Kavya"),
                    new Customer("Naveen"),
                    new Customer("Shruti"),
                    new Customer("Varun"),
                    new Customer("Meera"),
                    new Customer("Sneha"),
                    new Customer("Varun"),
                    new Customer("Arjun"));

                await _dbContext.SaveChangesAsync();

            }


        }
    }
}

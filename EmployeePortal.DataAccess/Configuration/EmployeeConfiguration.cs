using EmployeePortal.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.DataAccess.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Primary Key
            builder.HasKey(x => x.Id);

            //Properties 
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.MiddleName).HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.EmployedFrom).IsRequired();

            //One to One Relationship
            builder.HasOne(x => x.EmployeeDetail)
                   .WithOne(x => x.Employee)
                   .HasForeignKey<EmployeeDetail>(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

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
    public class EmployeeDetailConfiguration : IEntityTypeConfiguration<EmployeeDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeDetail> builder)
        {
            //Primary key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Age).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.PhoneNo).IsRequired();
            builder.Property(x => x.EmailAddress);
            builder.Property(x => x.Address).IsRequired();
        }
    }
}

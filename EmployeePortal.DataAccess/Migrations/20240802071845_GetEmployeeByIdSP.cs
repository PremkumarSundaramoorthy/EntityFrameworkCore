using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePortal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GetEmployeeByIdSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[GetEmployeeById]
              @EmployeeId INT
              AS
              BEGIN
                  SET NOCOUNT ON;
                  SELECT * FROM Employee WHERE Id = @EmployeeId
              END
              ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetEmployeeById]");
        }
    }
}

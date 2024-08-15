using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstPro.DAL.Migrations
{
    /// <inheritdoc />
    public partial class uploadfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Employees");
        }
    }
}

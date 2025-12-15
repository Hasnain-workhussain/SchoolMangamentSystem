using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMangamentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Grades");
        }
    }
}

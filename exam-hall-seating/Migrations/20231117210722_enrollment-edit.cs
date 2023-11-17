using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_hall_seating.Migrations
{
    /// <inheritdoc />
    public partial class enrollmentedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Enrollments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

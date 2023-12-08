using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_hall_seating.Migrations
{
    /// <inheritdoc />
    public partial class editclassroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Classrooms",
                newName: "Block");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Classrooms",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "Block",
                table: "Classrooms",
                newName: "Capacity");
        }
    }
}

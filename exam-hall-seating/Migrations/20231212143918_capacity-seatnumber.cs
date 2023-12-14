using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_hall_seating.Migrations
{
    /// <inheritdoc />
    public partial class capacityseatnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_ClassroomsDetail_ClassroomDetailId",
                table: "ExamSeats");

            migrationBuilder.DropIndex(
                name: "IX_ExamSeats_ClassroomDetailId",
                table: "ExamSeats");

            migrationBuilder.DropColumn(
                name: "ClassroomDetailId",
                table: "ExamSeats");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "ExamSeats",
                newName: "SeatNumber");

            migrationBuilder.RenameColumn(
                name: "Column",
                table: "ExamSeats",
                newName: "ClassroomId");

            migrationBuilder.AddColumn<int>(
                name: "ExamCapacity",
                table: "Classrooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeats_ClassroomId",
                table: "ExamSeats",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_Classrooms_ClassroomId",
                table: "ExamSeats",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_Classrooms_ClassroomId",
                table: "ExamSeats");

            migrationBuilder.DropIndex(
                name: "IX_ExamSeats_ClassroomId",
                table: "ExamSeats");

            migrationBuilder.DropColumn(
                name: "ExamCapacity",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "ExamSeats",
                newName: "Row");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "ExamSeats",
                newName: "Column");

            migrationBuilder.AddColumn<int>(
                name: "ClassroomDetailId",
                table: "ExamSeats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeats_ClassroomDetailId",
                table: "ExamSeats",
                column: "ClassroomDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_ClassroomsDetail_ClassroomDetailId",
                table: "ExamSeats",
                column: "ClassroomDetailId",
                principalTable: "ClassroomsDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

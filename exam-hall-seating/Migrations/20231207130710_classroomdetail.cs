using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_hall_seating.Migrations
{
    /// <inheritdoc />
    public partial class classroomdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_Classrooms_ClassroomId",
                table: "ExamSeats");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Column",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "ExamSeats",
                newName: "ClassroomDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSeats_ClassroomId",
                table: "ExamSeats",
                newName: "IX_ExamSeats_ClassroomDetailId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Classrooms",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "ClassroomsDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomId = table.Column<int>(type: "int", nullable: false),
                    BlockNo = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    ValidColumns = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassroomsDetail_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomsDetail_ClassroomId",
                table: "ClassroomsDetail",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_ClassroomsDetail_ClassroomDetailId",
                table: "ExamSeats",
                column: "ClassroomDetailId",
                principalTable: "ClassroomsDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_ClassroomsDetail_ClassroomDetailId",
                table: "ExamSeats");

            migrationBuilder.DropTable(
                name: "ClassroomsDetail");

            migrationBuilder.RenameColumn(
                name: "ClassroomDetailId",
                table: "ExamSeats",
                newName: "ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSeats_ClassroomDetailId",
                table: "ExamSeats",
                newName: "IX_ExamSeats_ClassroomId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Classrooms",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Block",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_Classrooms_ClassroomId",
                table: "ExamSeats",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

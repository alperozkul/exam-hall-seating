﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_hall_seating.Migrations
{
    /// <inheritdoc />
    public partial class enrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Enrollments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "Course");

            migrationBuilder.AddColumn<bool>(
                name: "Optional",
                table: "Course",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Optional",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "CourseType",
                table: "Course",
                nullable: false,
                defaultValue: 0);
        }
    }
}

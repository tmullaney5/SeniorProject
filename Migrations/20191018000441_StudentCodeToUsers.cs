using Microsoft.EntityFrameworkCore.Migrations;

namespace SightWordsProject.Migrations
{
    public partial class StudentCodeToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentCode",
                table: "AspNetUsers");
        }
    }
}

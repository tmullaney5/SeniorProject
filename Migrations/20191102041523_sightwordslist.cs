using Microsoft.EntityFrameworkCore.Migrations;

namespace SightWordsProject.Migrations
{
    public partial class sightwordslist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "WordsList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordsList",
                table: "WordsList",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WordsList",
                table: "WordsList");

            migrationBuilder.RenameTable(
                name: "WordsList",
                newName: "Words");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "ID");
        }
    }
}

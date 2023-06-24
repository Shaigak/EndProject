using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEndProjectCode.Migrations
{
    public partial class createauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Historys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Historys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Historys");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Historys");
        }
    }
}

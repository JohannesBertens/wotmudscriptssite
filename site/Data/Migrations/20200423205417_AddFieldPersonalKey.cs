using Microsoft.EntityFrameworkCore.Migrations;

namespace site.Data.Migrations
{
    public partial class AddFieldPersonalKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalKey",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalKey",
                table: "AspNetUsers");
        }
    }
}

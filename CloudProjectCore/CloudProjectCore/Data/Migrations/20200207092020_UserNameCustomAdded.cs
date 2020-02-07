using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudProjectCore.Data.Migrations
{
    public partial class UserNameCustomAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyUserName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyUserName",
                table: "AspNetUsers");
        }
    }
}

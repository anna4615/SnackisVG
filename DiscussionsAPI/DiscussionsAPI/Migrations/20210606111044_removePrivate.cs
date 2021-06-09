using Microsoft.EntityFrameworkCore.Migrations;

namespace PostsAPI.Migrations
{
    public partial class removePrivate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "OnlyForUserName",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OnlyForUserName",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

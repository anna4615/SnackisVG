using Microsoft.EntityFrameworkCore.Migrations;

namespace PostsAPI.Migrations
{
    public partial class LikeLove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfLike",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLove",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfLike",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "NumberOfLove",
                table: "Post");
        }
    }
}

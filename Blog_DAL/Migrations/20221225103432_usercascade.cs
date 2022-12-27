using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog_DAL.Migrations
{
    public partial class usercascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_Author_id",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_Author_id",
                table: "Post",
                column: "Author_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_Author_id",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_Author_id",
                table: "Post",
                column: "Author_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

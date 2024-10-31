using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieVault.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkNowInMovieObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_UserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UserId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_IdentityUserId",
                table: "Movies",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_IdentityUserId",
                table: "Movies",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_IdentityUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_IdentityUserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

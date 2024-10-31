﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieVault.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movies");
        }
    }
}
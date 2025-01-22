﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticUniversity.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedRatingToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "comments");
        }
    }
}

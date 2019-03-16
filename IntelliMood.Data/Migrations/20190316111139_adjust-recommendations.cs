using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IntelliMood.Data.Migrations
{
    public partial class adjustrecommendations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "UserRecommendations");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "UserRecommendations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserRecommendations");

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "UserRecommendations",
                nullable: false,
                defaultValue: false);
        }
    }
}

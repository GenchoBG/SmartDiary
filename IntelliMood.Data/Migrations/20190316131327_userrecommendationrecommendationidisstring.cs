using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IntelliMood.Data.Migrations
{
    public partial class userrecommendationrecommendationidisstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RecommendationId",
                table: "UserRecommendations",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecommendationId",
                table: "UserRecommendations",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class ProfitAndLoss_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Brands_BrandId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Brands");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandId",
                table: "Brands",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Brands_BrandId",
                table: "Brands",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

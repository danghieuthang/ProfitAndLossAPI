using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class ProfitAndLost_V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stores_BrandId",
                table: "Stores",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Brands_BrandId",
                table: "Stores",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Brands_BrandId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_BrandId",
                table: "Stores");
        }
    }
}

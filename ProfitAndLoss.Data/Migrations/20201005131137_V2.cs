using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountPeriodDetailId",
                table: "TransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountingPeriodDetailId",
                table: "TransactionDetails",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_AccountPeriodDetailId",
                table: "TransactionDetails",
                column: "AccountPeriodDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPeriodDetails_StoreId",
                table: "AccountPeriodDetails",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPeriodDetails_Stores_StoreId",
                table: "AccountPeriodDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_AccountPeriodDetails_AccountPeriodDetailId",
                table: "TransactionDetails",
                column: "AccountPeriodDetailId",
                principalTable: "AccountPeriodDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountPeriodDetails_Stores_StoreId",
                table: "AccountPeriodDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_AccountPeriodDetails_AccountPeriodDetailId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_AccountPeriodDetailId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_AccountPeriodDetails_StoreId",
                table: "AccountPeriodDetails");

            migrationBuilder.DropColumn(
                name: "AccountPeriodDetailId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "AccountingPeriodDetailId",
                table: "TransactionDetails");
        }
    }
}

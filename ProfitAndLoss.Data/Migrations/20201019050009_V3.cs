using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evidences_Recepts_ReceptId",
                table: "Evidences");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Recepts_ReceptId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Recepts");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ReceptId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Evidences_ReceptId",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "ReceptId",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiptId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiptId",
                table: "Evidences",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    CreateMemberId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TypeId = table.Column<Guid>(nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReceiptId",
                table: "Transactions",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_ReceiptId",
                table: "Evidences",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_StoreId",
                table: "Receipts",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evidences_Receipts_ReceiptId",
                table: "Evidences",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Receipts_ReceiptId",
                table: "Transactions",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evidences_Receipts_ReceiptId",
                table: "Evidences");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Receipts_ReceiptId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ReceiptId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Evidences_ReceiptId",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Evidences");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceptId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Recepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false),
                    CreateMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recepts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReceptId",
                table: "Transactions",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_ReceptId",
                table: "Evidences",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepts_StoreId",
                table: "Recepts",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evidences_Recepts_ReceptId",
                table: "Evidences",
                column: "ReceptId",
                principalTable: "Recepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Recepts_ReceptId",
                table: "Transactions",
                column: "ReceptId",
                principalTable: "Recepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

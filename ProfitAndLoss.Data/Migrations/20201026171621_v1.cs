using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("a921f5ef-229d-4628-b70f-11640c38e4df"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("016d8d62-a59a-4805-ac32-8921d8bbca5d"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("5224fd84-3843-4976-b461-325aad021e20"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("6c892ded-a7d3-4cb6-bad1-3d0a10db4de3"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("ac360862-f7da-4816-93c8-31cb52d9b381"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionTypeId",
                table: "TransactionCategories",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2020, 10, 27, 0, 16, 20, 202, DateTimeKind.Local).AddTicks(824), new DateTime(2020, 10, 27, 0, 16, 20, 202, DateTimeKind.Local).AddTicks(862) });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Actived", "BrandId", "Code", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new Guid("bd1d2296-42cb-4e75-bfbd-0933c476e5ee"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HCM-01", new DateTime(2020, 10, 27, 0, 16, 20, 202, DateTimeKind.Local).AddTicks(7981), new DateTime(2020, 10, 27, 0, 16, 20, 202, DateTimeKind.Local).AddTicks(8890), "Văn phòng quyền lực HCM" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Actived", "CreatedDate", "IsDebit", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"), true, new DateTime(2020, 10, 27, 0, 16, 20, 197, DateTimeKind.Local).AddTicks(2469), true, new DateTime(2020, 10, 27, 0, 16, 20, 198, DateTimeKind.Local).AddTicks(8600), "Sales" },
                    { new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"), true, new DateTime(2020, 10, 27, 0, 16, 20, 199, DateTimeKind.Local).AddTicks(244), true, new DateTime(2020, 10, 27, 0, 16, 20, 199, DateTimeKind.Local).AddTicks(306), "Revenues" },
                    { new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"), true, new DateTime(2020, 10, 27, 0, 16, 20, 199, DateTimeKind.Local).AddTicks(331), true, new DateTime(2020, 10, 27, 0, 16, 20, 199, DateTimeKind.Local).AddTicks(333), "Invoice" },
                    { new Guid("4e44153a-8703-4500-8d7c-a46048a5f2f5"), true, new DateTime(2020, 10, 27, 0, 16, 20, 199, DateTimeKind.Local).AddTicks(340), true, new DateTime(2020, 10, 27, 0, 16, 20, 199, DateTimeKind.Local).AddTicks(342), "Expenses" }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Actived", "Code", "CreatedDate", "Description", "ModifiedDate", "Name", "TransactionTypeId" },
                values: new object[,]
                {
                    { new Guid("c2f55e42-f7af-4f23-a0b0-02cbe2165e58"), true, "Sale-001", new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(5998), null, new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6031), "Product Sale", new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52") },
                    { new Guid("8bbbfde6-03be-4b8a-8c03-ebf6d1e77684"), true, "Sale-001", new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6210), null, new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6213), "Room Revenues", new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9") },
                    { new Guid("7be8981f-fc33-4a9a-a099-861220405ed0"), true, "Invoice-001", new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6228), null, new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6230), "Inventory Expense", new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48") },
                    { new Guid("3af494df-da63-44cc-a20c-ee0dd0775ac8"), true, "Expense-001", new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6237), null, new DateTime(2020, 10, 27, 0, 16, 20, 203, DateTimeKind.Local).AddTicks(6239), "Wages Expense", new Guid("4e44153a-8703-4500-8d7c-a46048a5f2f5") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("bd1d2296-42cb-4e75-bfbd-0933c476e5ee"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("3af494df-da63-44cc-a20c-ee0dd0775ac8"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("7be8981f-fc33-4a9a-a099-861220405ed0"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("8bbbfde6-03be-4b8a-8c03-ebf6d1e77684"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("c2f55e42-f7af-4f23-a0b0-02cbe2165e58"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("4e44153a-8703-4500-8d7c-a46048a5f2f5"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionTypeId",
                table: "TransactionCategories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2020, 10, 25, 10, 46, 40, 214, DateTimeKind.Local).AddTicks(1063), new DateTime(2020, 10, 25, 10, 46, 40, 214, DateTimeKind.Local).AddTicks(1108) });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Actived", "BrandId", "Code", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new Guid("a921f5ef-229d-4628-b70f-11640c38e4df"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HCM-01", new DateTime(2020, 10, 25, 10, 46, 40, 215, DateTimeKind.Local).AddTicks(7730), new DateTime(2020, 10, 25, 10, 46, 40, 215, DateTimeKind.Local).AddTicks(9471), "Văn phòng quyền lực HCM" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Actived", "CreatedDate", "IsDebit", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("ac360862-f7da-4816-93c8-31cb52d9b381"), true, new DateTime(2020, 10, 25, 10, 46, 40, 209, DateTimeKind.Local).AddTicks(7799), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(3832), "Sales" },
                    { new Guid("016d8d62-a59a-4805-ac32-8921d8bbca5d"), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(5313), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(5381), "Revenues" },
                    { new Guid("6c892ded-a7d3-4cb6-bad1-3d0a10db4de3"), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(5465), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(5468), "Expenses" },
                    { new Guid("5224fd84-3843-4976-b461-325aad021e20"), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(5472), true, new DateTime(2020, 10, 25, 10, 46, 40, 211, DateTimeKind.Local).AddTicks(5474), "Invoice" }
                });
        }
    }
}

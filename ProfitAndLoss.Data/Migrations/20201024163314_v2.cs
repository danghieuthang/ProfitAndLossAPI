using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("14702701-559c-45ee-8e30-ffb7bb64ee65"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("7c044b86-5a40-4849-bb0a-c6aadc19444e"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("828ee74b-df7d-49e1-ad65-cae8603a71e6"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("cfe2838e-11ef-4761-bf84-d0c0a2df29cc"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("e38cd5e2-d55d-42f9-9e85-6cd28a6e9045"));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Receipts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Receipts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2020, 10, 24, 23, 33, 11, 780, DateTimeKind.Local).AddTicks(8209), new DateTime(2020, 10, 24, 23, 33, 11, 780, DateTimeKind.Local).AddTicks(8250) });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Actived", "BrandId", "Code", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new Guid("d143b569-6a5a-4da6-8d9e-d144af7f5f9b"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HCM-01", new DateTime(2020, 10, 24, 23, 33, 11, 781, DateTimeKind.Local).AddTicks(8501), new DateTime(2020, 10, 24, 23, 33, 11, 781, DateTimeKind.Local).AddTicks(9398), "Cửa hàng quyền lực HCM" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Actived", "CreatedDate", "IsDebit", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("89cb4dcc-0ced-4c86-ad5f-fef3e96bbb04"), true, new DateTime(2020, 10, 24, 23, 33, 11, 776, DateTimeKind.Local).AddTicks(6915), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(1960), "Sales" },
                    { new Guid("8bfe6b95-f6aa-4ed2-9b2f-c7e562fa857f"), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(3120), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(3169), "Revenues" },
                    { new Guid("7ca9746d-31bc-406a-9c5b-c6dad555cacd"), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(3235), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(3238), "Expenses" },
                    { new Guid("0aa150f8-c776-41ad-a974-230d1222a9b5"), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(3242), true, new DateTime(2020, 10, 24, 23, 33, 11, 778, DateTimeKind.Local).AddTicks(3243), "Invoice" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("d143b569-6a5a-4da6-8d9e-d144af7f5f9b"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("0aa150f8-c776-41ad-a974-230d1222a9b5"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("7ca9746d-31bc-406a-9c5b-c6dad555cacd"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("89cb4dcc-0ced-4c86-ad5f-fef3e96bbb04"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("8bfe6b95-f6aa-4ed2-9b2f-c7e562fa857f"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Receipts");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2020, 10, 24, 23, 19, 9, 463, DateTimeKind.Local).AddTicks(8548), new DateTime(2020, 10, 24, 23, 19, 9, 463, DateTimeKind.Local).AddTicks(8577) });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Actived", "BrandId", "Code", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new Guid("14702701-559c-45ee-8e30-ffb7bb64ee65"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HCM-01", new DateTime(2020, 10, 24, 23, 19, 9, 464, DateTimeKind.Local).AddTicks(7125), new DateTime(2020, 10, 24, 23, 19, 9, 464, DateTimeKind.Local).AddTicks(7856), "Cửa hàng quyền lực HCM" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Actived", "CreatedDate", "IsDebit", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("828ee74b-df7d-49e1-ad65-cae8603a71e6"), true, new DateTime(2020, 10, 24, 23, 19, 9, 459, DateTimeKind.Local).AddTicks(8638), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(4998), "Sales" },
                    { new Guid("e38cd5e2-d55d-42f9-9e85-6cd28a6e9045"), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(8174), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(8266), "Revenues" },
                    { new Guid("7c044b86-5a40-4849-bb0a-c6aadc19444e"), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(8311), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(8314), "Expenses" },
                    { new Guid("cfe2838e-11ef-4761-bf84-d0c0a2df29cc"), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(8317), true, new DateTime(2020, 10, 24, 23, 19, 9, 461, DateTimeKind.Local).AddTicks(8319), "Invoice" }
                });
        }
    }
}

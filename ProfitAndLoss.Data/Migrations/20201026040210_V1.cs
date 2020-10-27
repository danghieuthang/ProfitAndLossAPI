using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class V1 : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "TransactionTypes",
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2020, 10, 26, 11, 2, 9, 640, DateTimeKind.Local).AddTicks(8197), new DateTime(2020, 10, 26, 11, 2, 9, 640, DateTimeKind.Local).AddTicks(8230) });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Actived", "BrandId", "Code", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new Guid("2fd3c5c9-8947-4bc3-8ee4-972175def3e0"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HCM-01", new DateTime(2020, 10, 26, 11, 2, 9, 641, DateTimeKind.Local).AddTicks(9051), new DateTime(2020, 10, 26, 11, 2, 9, 641, DateTimeKind.Local).AddTicks(9993), "Văn phòng quyền lực HCM" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Actived", "Code", "CreatedDate", "IsDebit", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("4e44153a-8703-4500-8d7c-a46048a5f2f5"), true, null, new DateTime(2020, 10, 26, 11, 2, 9, 636, DateTimeKind.Local).AddTicks(4177), true, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(7526), "Sales" },
                    { new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"), true, null, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(8626), true, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(8722), "Revenues" },
                    { new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"), true, null, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(8742), true, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(8744), "Expenses" },
                    { new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"), true, null, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(8748), true, new DateTime(2020, 10, 26, 11, 2, 9, 637, DateTimeKind.Local).AddTicks(8750), "Invoice" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("2fd3c5c9-8947-4bc3-8ee4-972175def3e0"));

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

            migrationBuilder.DropColumn(
                name: "Code",
                table: "TransactionTypes");

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

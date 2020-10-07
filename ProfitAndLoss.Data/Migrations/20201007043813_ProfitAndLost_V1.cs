using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class ProfitAndLost_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Actors",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TransactionHistories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "TransactionDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "StoreAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Recepts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "MemberStores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MemberStores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MemberStores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Members",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Feedbacks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Actors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Actors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Actors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Accounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TransactionHistories");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "StoreAccounts");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Recepts");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "MemberStores");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MemberStores");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MemberStores");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Actors",
                newName: "ID");
        }
    }
}

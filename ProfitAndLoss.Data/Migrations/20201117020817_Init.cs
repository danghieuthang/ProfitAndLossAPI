using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfitAndLoss.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Fullname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    IsDebit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountingPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingPeriods_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TransactionTypeId = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    IsDebit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCategories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionCategories_ReceiptTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "ReceiptTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountingPeriodInStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    StartedDate = table.Column<DateTime>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    AccountingPeriodId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingPeriodInStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingPeriodInStore_AccountingPeriods_AccountingPeriodId",
                        column: x => x.AccountingPeriodId,
                        principalTable: "AccountingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingPeriodInStore_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 255, nullable: false),
                    StoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreAccounts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountingPeriodId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AccountingPeriods_AccountingPeriodId",
                        column: x => x.AccountingPeriodId,
                        principalTable: "AccountingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    StoreId = table.Column<Guid>(nullable: true),
                    CreateMemberId = table.Column<Guid>(nullable: true),
                    ReceiptTypeId = table.Column<Guid>(nullable: true),
                    TotalBalance = table.Column<double>(nullable: false),
                    ShippingFee = table.Column<double>(nullable: false),
                    DiscountPercent = table.Column<double>(nullable: false),
                    DiscountValue = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    AmountPaid = table.Column<double>(nullable: false),
                    DueBalance = table.Column<double>(nullable: false),
                    TermExport = table.Column<DateTime>(nullable: false),
                    OpenDate = table.Column<DateTime>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    NoteMessage = table.Column<string>(nullable: true),
                    SupplierId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Members_CreateMemberId",
                        column: x => x.CreateMemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_ReceiptTypes_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalTable: "ReceiptTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evidences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ImgUrl = table.Column<string>(maxLength: 255, nullable: true),
                    ReceiptId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidences_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(maxLength: 2000, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReceiptId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptHistories_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    ReceiptId = table.Column<Guid>(nullable: true),
                    TransactionCategoryId = table.Column<Guid>(nullable: true),
                    AccountingPeriodInStoreId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AccountingPeriodInStore_AccountingPeriodInStoreId",
                        column: x => x.AccountingPeriodInStoreId,
                        principalTable: "AccountingPeriodInStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Actived", "Code", "CreatedDate", "ModifiedDate" },
                values: new object[] { new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), true, "B-PL", new DateTime(2020, 11, 17, 9, 8, 16, 168, DateTimeKind.Local).AddTicks(9742), new DateTime(2020, 11, 17, 9, 8, 16, 168, DateTimeKind.Local).AddTicks(9775) });

            migrationBuilder.InsertData(
                table: "ReceiptTypes",
                columns: new[] { "Id", "Actived", "Code", "CreatedDate", "IsDebit", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"), true, "SAL", new DateTime(2020, 11, 17, 9, 8, 16, 164, DateTimeKind.Local).AddTicks(5607), true, new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(2934), "Sales" },
                    { new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"), true, "REV", new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(5397), true, new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(5462), "Revenues" },
                    { new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"), true, "INV", new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(5508), false, new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(5512), "Invoice" },
                    { new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e"), true, "EXP", new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(5519), false, new DateTime(2020, 11, 17, 9, 8, 16, 166, DateTimeKind.Local).AddTicks(5522), "Expenses" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Actived", "Address", "CreatedDate", "Email", "ModifiedDate", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("5b9509b4-5576-459b-921f-6e5a53a21b8b"), true, "This is address of hp company", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(6566), "hpcompany@hp.com", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(6576), "HP", "090022333" },
                    { new Guid("783c7b9d-f927-4ea2-a106-34e89f32114e"), true, "This is address of Dell company", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(6657), "dellcompany@dell.com", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(6659), "Dell", "0977737014" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Actived", "BrandId", "Code", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("fdbe21a6-773e-4d21-be00-b12b5ecc5333"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HCM-01", new DateTime(2020, 11, 17, 9, 8, 16, 169, DateTimeKind.Local).AddTicks(6522), new DateTime(2020, 11, 17, 9, 8, 16, 169, DateTimeKind.Local).AddTicks(7283), "Văn phòng quyền lực HCM" },
                    { new Guid("d9947257-c13d-4fa2-aae7-4e6c73f59f13"), true, new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), "HN-01", new DateTime(2020, 11, 17, 9, 8, 16, 169, DateTimeKind.Local).AddTicks(8082), new DateTime(2020, 11, 17, 9, 8, 16, 169, DateTimeKind.Local).AddTicks(8100), "Văn phòng quyền lực Hà Nội" }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "AccountId", "Actived", "Code", "CreatedDate", "Description", "IsDebit", "ModifiedDate", "Name", "TransactionTypeId" },
                values: new object[,]
                {
                    { new Guid("8ddb84cc-0c1f-46b5-8c47-57e22b9c1aa5"), null, true, "SAL-PS", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(1722), null, true, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(1732), "Product Sale", new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52") },
                    { new Guid("9cf0ca58-8245-4c16-a19e-bcd4a75ab5c7"), null, true, "SAL-COGS", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2515), null, false, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2522), "Cost of goods sold", new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52") },
                    { new Guid("8a9773ca-6963-4462-a70e-d50033a2d4b4"), null, true, "SAL-DIS", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2627), null, false, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2629), "Discount", new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52") },
                    { new Guid("3c8a93b7-f477-4b59-8b68-79239ae4f11c"), null, true, "REV-COMMON", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2552), null, true, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2553), "Common Revenues", new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9") },
                    { new Guid("c5626da0-7dbf-4015-bf9d-2cc8daf42ce7"), null, true, "REV-PAKING", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2562), null, true, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2564), "Paking Revenues", new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9") },
                    { new Guid("cfc3145a-6eca-428b-9d68-4da631c54a18"), null, true, "REV-BRAND", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2603), null, true, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2604), "Brand Revenues", new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9") },
                    { new Guid("16e81eae-8b55-4acd-9202-7e5b0fb91688"), null, true, "INV-INVENTORY", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2543), null, false, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2544), "Inventory", new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48") },
                    { new Guid("4f4a9916-3388-4c29-a734-04246304c60c"), null, true, "EXP-WAGES", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2611), null, false, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2613), "Wages Expense", new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e") },
                    { new Guid("6a0eec6e-8f35-4dce-91e9-4cd2fba41747"), null, true, "EXP-SHIP", new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2619), null, false, new DateTime(2020, 11, 17, 9, 8, 16, 170, DateTimeKind.Local).AddTicks(2620), "Shipping Fee", new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriodInStore_AccountingPeriodId",
                table: "AccountingPeriodInStore",
                column: "AccountingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriodInStore_StoreId",
                table: "AccountingPeriodInStore",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriods_BrandId",
                table: "AccountingPeriods",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BrandId",
                table: "Accounts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_ReceiptId",
                table: "Evidences",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AccountingPeriodId",
                table: "Feedbacks",
                column: "AccountingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_MemberId",
                table: "Feedbacks",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_StoreId",
                table: "Members",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptHistories_ReceiptId",
                table: "ReceiptHistories",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CreateMemberId",
                table: "Receipts",
                column: "CreateMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ReceiptTypeId",
                table: "Receipts",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_StoreId",
                table: "Receipts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SupplierId",
                table: "Receipts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreAccounts_AccountId",
                table: "StoreAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreAccounts_StoreId",
                table: "StoreAccounts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_BrandId",
                table: "Stores",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_AccountId",
                table: "TransactionCategories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_TransactionTypeId",
                table: "TransactionCategories",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountingPeriodInStoreId",
                table: "Transactions",
                column: "AccountingPeriodInStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReceiptId",
                table: "Transactions",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Evidences");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "ReceiptHistories");

            migrationBuilder.DropTable(
                name: "StoreAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AccountingPeriodInStore");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropTable(
                name: "AccountingPeriods");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ReceiptTypes");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}

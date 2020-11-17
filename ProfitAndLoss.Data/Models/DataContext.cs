using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, string>
    {

        #region Dbsets

        public DbSet<Member> Members { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Receipt> Transactions { get; set; }
        public DbSet<Transaction> TransactionDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<ReceiptHistory> TransactionHistories { get; set; }
        public DbSet<ReceiptType> TransactionTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountingPeriod> AccountingPeriods { get; set; }
        public DbSet<AccountingPeriodInStore> AccountingPeriodInStores { get; set; }
        public DbSet<StoreAccount> StoreAccounts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }



        #endregion Dbsets

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region create relationship

            //modelBuilder.Entity<MemberStore>().HasKey(ms => new { ms.MemberId, ms.StoreId });

            //modelBuilder.Entity<MemberStore>()
            //    .HasOne<Store>(ms => ms.Store)
            //    .WithMany(s => s.MemberStores)
            //    .HasForeignKey(ms => ms.StoreId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<MemberStore>()
            //    .HasOne<Member>(ms => ms.Member)
            //    .WithMany(m => m.MemberStores)
            //    .HasForeignKey(ms => ms.MemberId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<StoreAccount>().HasKey(sa => new { sa.AccountId, sa.StoreId });

            //modelBuilder.Entity<StoreAccount>()
            //     .HasOne<Store>(sa => sa.Store)
            //     .WithMany(s => s.StoreAccounts)
            //     .HasForeignKey(sa => sa.StoreId)
            //     .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<StoreAccount>()
            //     .HasOne<Account>(sa => sa.Account)
            //     .WithMany(s => s.StoreAccounts)
            //     .HasForeignKey(sa => sa.AccountId)
            //     .OnDelete(DeleteBehavior.NoAction);

            #endregion create relationship
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<ReceiptType>().HasData(
                new ReceiptType { Id = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Sales", ModifiedDate = DateTime.Now, Code = "SAL" },
                new ReceiptType { Id = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Revenues", ModifiedDate = DateTime.Now, Code = "REV" },
                new ReceiptType { Id = new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"), Actived = true, CreatedDate = DateTime.Now, IsDebit = false, Name = "Invoice", ModifiedDate = DateTime.Now, Code = "INV" },
                new ReceiptType { Id = new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e"), Actived = true, CreatedDate = DateTime.Now, IsDebit = false, Name = "Expenses", ModifiedDate = DateTime.Now, Code = "EXP" }
                );
            modelBuilder.Entity<Brand>().HasData(
                new Brand()
                {
                    Id = new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                    Code = "B-PL",
                    Actived = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = Guid.NewGuid(),
                    BrandId = new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                    Actived = true,
                    CreatedDate = DateTime.Now,
                    Code = "HCM-01",
                    ModifiedDate = DateTime.Now,
                    Name = "Văn phòng quyền lực HCM"
                },
                 new Store
                 {
                     Id = Guid.NewGuid(),
                     BrandId = new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
                     Actived = true,
                     CreatedDate = DateTime.Now,
                     Code = "HN-01",
                     ModifiedDate = DateTime.Now,
                     Name = "Văn phòng quyền lực Hà Nội"
                 }
                );

            _ = modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"),
                    Actived = true,
                    Code = "SAL-PS",
                    Name = "Product Sale",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = new Guid("8DDB84CC-0C1F-46B5-8C47-57E22B9C1AA5"),
                    IsDebit = true
                },
                 new TransactionCategory()
                 {
                     TransactionTypeId = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"),
                     Actived = true,
                     Code = "SAL-COGS",
                     Name = "Cost of goods sold",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     Id = new Guid("9CF0CA58-8245-4C16-A19E-BCD4A75AB5C7"),
                     IsDebit = false,
                 },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"),
                    Actived = true,
                    Code = "INV-INVENTORY",
                    Name = "Inventory",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = new Guid("16E81EAE-8B55-4ACD-9202-7E5B0FB91688"),
                    IsDebit = false
                },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"),
                    Actived = true,
                    Code = "REV-COMMON",
                    Name = "Common Revenues",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = new Guid("3C8A93B7-F477-4B59-8B68-79239AE4F11C"),
                    IsDebit = true
                },
                 new TransactionCategory()
                 {
                     TransactionTypeId = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"),
                     Actived = true,
                     Code = "REV-PAKING",
                     Name = "Paking Revenues",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     Id = new Guid("C5626DA0-7DBF-4015-BF9D-2CC8DAF42CE7"),
                     IsDebit = true
                 },
                  new TransactionCategory()
                  {
                      TransactionTypeId = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"),
                      Actived = true,
                      Code = "REV-BRAND",
                      Name = "Brand Revenues",
                      CreatedDate = DateTime.Now,
                      ModifiedDate = DateTime.Now,
                      Id = new Guid("CFC3145A-6ECA-428B-9D68-4DA631C54A18"),
                      IsDebit = true,
                  },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e"),
                    Actived = true,
                    Code = "EXP-WAGES",
                    Name = "Wages Expense", //  Tiền lương
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = new Guid("4F4A9916-3388-4C29-A734-04246304C60C"),
                    IsDebit = false
                },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e"),
                    Actived = true,
                    Code = "EXP-SHIP",
                    Name = "Shipping Fee",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = new Guid("6A0EEC6E-8F35-4DCE-91E9-4CD2FBA41747"),
                    IsDebit = false
                },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"),
                    Actived = true,
                    Code = "SAL-DIS",
                    Name = "Discount",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = new Guid("8A9773CA-6963-4462-A70E-D50033A2D4B4"),
                    IsDebit = false
                }
                );

            modelBuilder.Entity<Supplier>().HasData(
               new Supplier
               {
                   Id = Guid.NewGuid(),
                   Actived = true,
                   Name = "HP",
                   Email = "hpcompany@hp.com",
                   Phone = "090022333",
                   Address = "This is address of hp company",
                   CreatedDate = DateTime.Now,
                   ModifiedDate = DateTime.Now
               },
                new Supplier
                {
                    Id = Guid.NewGuid(),
                    Actived = true,
                    Name = "Dell",
                    Email = "dellcompany@dell.com",
                    Phone = "0977737014",
                    Address = "This is address of Dell company",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
               );
        }
    }
}

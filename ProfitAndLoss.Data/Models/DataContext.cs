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

        public virtual DbSet<Actor> Actors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
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

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType { Id = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Sales", ModifiedDate = DateTime.Now, Code = "SAL" },
                new TransactionType { Id = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Revenues", ModifiedDate = DateTime.Now, Code = "REV" },
                new TransactionType { Id = new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"), Actived = true, CreatedDate = DateTime.Now, IsDebit = false, Name = "Invoice", ModifiedDate = DateTime.Now, Code = "INV" },
                new TransactionType { Id = new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e"), Actived = true, CreatedDate = DateTime.Now, IsDebit = false, Name = "Expenses", ModifiedDate = DateTime.Now, Code = "EXP" }
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
                    Id = Guid.NewGuid(),
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
                     Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
                    IsDebit = false
                },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"),
                    Actived = true,
                    Code = "INV-DIS",
                    Name = "Inventory Discount",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsDebit = true
                },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"),
                    Actived = true,
                    Code = "REV-COMMON",
                    Name = "Common Revenues",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
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
                     Id = Guid.NewGuid(),
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
                      Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
                    IsDebit = false
                },
                new TransactionCategory()
                {
                    TransactionTypeId = new Guid("d59d5f6c-5fc1-4977-8f17-a8f78556bf6e"),
                    Actived = true,
                    Code = "EXP-DIS",
                    Name = "Discount",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
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

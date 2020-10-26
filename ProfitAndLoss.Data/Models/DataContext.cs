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
                new TransactionType { Id = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Sales", ModifiedDate = DateTime.Now },
                new TransactionType { Id = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Revenues", ModifiedDate = DateTime.Now },
                new TransactionType { Id = new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Invoice", ModifiedDate = DateTime.Now },
                new TransactionType { Id = new Guid("4e44153a-8703-4500-8d7c-a46048a5f2f5"), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Expenses", ModifiedDate = DateTime.Now }
                );
            modelBuilder.Entity<Brand>().HasData(
                new Brand() { Id = new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), Actived = true,
                CreatedDate = DateTime.Now, ModifiedDate  = DateTime.Now});
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = Guid.NewGuid(), BrandId = new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"), Actived  = true, CreatedDate = DateTime.Now, Code = "HCM-01",
                ModifiedDate = DateTime.Now, Name = "Văn phòng quyền lực HCM"}
                );

            modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory() {
                    TransactionTypeId = new Guid("befe9e61-30c9-4594-8a26-5672d1d66e52"),  
                    Actived = true, Code = "Sale-001",
                    Name = "Product Sale",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid()
                    },
                new TransactionCategory() {
                    TransactionTypeId = new Guid("e4b06925-d89f-41ae-a495-5db8ab3dcfe9"),  
                    Actived = true, Code = "Sale-001",
                    Name = "Room Revenues",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid()
                    },
                new TransactionCategory() {
                    TransactionTypeId = new Guid("c1684003-c94f-4c7e-af92-5fc31c4efa48"),  
                    Actived = true, Code = "Invoice-001",
                    Name = "Inventory Expense",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid()
                    },
                new TransactionCategory() {
                    TransactionTypeId = new Guid("4e44153a-8703-4500-8d7c-a46048a5f2f5"),  
                    Actived = true, Code = "Expense-001",
                    Name = "Wages Expense",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Id = Guid.NewGuid()
                    }
                );

        }
    }
}

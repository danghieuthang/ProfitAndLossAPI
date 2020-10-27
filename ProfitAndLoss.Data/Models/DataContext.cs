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
                new TransactionType { Id = Guid.NewGuid(), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Sales", ModifiedDate = DateTime.Now },
                new TransactionType { Id = Guid.NewGuid(), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Revenues", ModifiedDate = DateTime.Now },
                new TransactionType { Id = Guid.NewGuid(), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Expenses", ModifiedDate = DateTime.Now },
                new TransactionType { Id = Guid.NewGuid(), Actived = true, CreatedDate = DateTime.Now, IsDebit = true, Name = "Invoice", ModifiedDate = DateTime.Now }
                );
            modelBuilder.Entity<Brand>().HasData(
                new Brand()
                {
                    Id = new Guid("05fe5bba-65ad-4b71-a5dd-08d878376f22"),
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

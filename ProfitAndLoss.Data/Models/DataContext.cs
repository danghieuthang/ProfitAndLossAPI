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
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionDetail> LedgerEntries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountingPeriod> AccountingPeriods { get; set; }
        public DbSet<MemberStore> MemberStores { get; set; }
        public DbSet<AccountingPeriodDetail> AccountPeriodDetails { get; set; }
        public DbSet<StoreAccount> StoreAccounts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }



        #endregion Dbsets

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region create relationship

            modelBuilder.Entity<MemberStore>().HasKey(ms => new { ms.MemberId, ms.StoreId });

            modelBuilder.Entity<MemberStore>()
                .HasOne<Store>(ms => ms.Store)
                .WithMany(s => s.MemberStores)
                .HasForeignKey(ms => ms.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemberStore>()
                .HasOne<Member>(ms => ms.Member)
                .WithMany(m => m.MemberStores)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoreAccount>().HasKey(sa => new { sa.AccountId, sa.StoreId });

            modelBuilder.Entity<StoreAccount>()
                 .HasOne<Store>(sa => sa.Store)
                 .WithMany(s => s.StoreAccounts)
                 .HasForeignKey(sa => sa.StoreId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoreAccount>()
                 .HasOne<Account>(sa => sa.Account)
                 .WithMany(s => s.StoreAccounts)
                 .HasForeignKey(sa => sa.AccountId)
                 .OnDelete(DeleteBehavior.NoAction);

            #endregion create relationship

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

        }

    }
}

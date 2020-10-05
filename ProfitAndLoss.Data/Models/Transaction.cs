using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        public Transaction()
        {

        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("Member")]
        public Guid CreateMemberId { get; set; }

        [ForeignKey("Store")]
        public Guid StoreId { get; set; }

        [ForeignKey("Transaction")]
        public Guid MasterTransactionId { get; set; }

        [ForeignKey("Type")]
        public Guid TypeId { get; set; }

        [DefaultValue(0)]
        public decimal Price { get; set; }

        [MaxLength(2000)]
        public string NoteMessage { get; set; }

        public virtual ICollection<LedgerEntry> LedgerEntries { get; set; }

        public virtual ICollection<Recept> Recepts { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }

        public virtual ICollection<Transaction> ChildTransactions { get; set; }

    }
}

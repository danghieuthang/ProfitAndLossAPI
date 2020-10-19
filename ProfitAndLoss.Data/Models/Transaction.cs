using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Transactions")]
    public class Transaction : BaseEntity<Guid>
    {
        public Transaction()
        {

        }

        [ForeignKey("Store")]
        public Guid StoreId { get; set; }

        [ForeignKey("Transaction")]
        public Guid MasterTransactionId { get; set; }

        [ForeignKey("Type")]
        public Guid TypeId { get; set; }

        [DefaultValue(0)]
        public decimal Price { get; set; }

        public string NoteMessage { get; set; }

        [ForeignKey("Receipt")]
        public Guid ReceiptId { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }

        public virtual ICollection<Transaction> ChildTransactions { get; set; }

    }
}

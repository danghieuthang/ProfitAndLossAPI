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

        [MaxLength(255)]
        public string Code { get; set; }

        [ForeignKey("Store")]
        public Guid StoreId { get; set; }

        public virtual Store Store { get; set; }

        [ForeignKey("Member")]
        public Guid CreateMemberId { get; set; }

        public virtual Member Member { get; set; }

        [ForeignKey("TransactionType")]
        public Guid TransactionTypeId { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        [DefaultValue(0)]
        public double Balance { get; set; }

        public string NoteMessage { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }

    }
}

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
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public double Balance { get; set; }

        [ForeignKey("ReceiptId")]
        public Guid? ReceiptId { get; set; }

        public virtual Receipt Receipt { get; set; }

        [ForeignKey("TransactionCategory")]
        public Guid? TransactionCategoryId { get; set; }

        public virtual TransactionCategory TransactionCategory { get; set; }

        [ForeignKey("AccountingPeriodInStore")]
        public Guid? AccountingPeriodInStoreId { get; set; }

        public virtual AccountingPeriodInStore AccountingPeriodInStore { get; set; }

        public int Status { get; set; }

    }
}

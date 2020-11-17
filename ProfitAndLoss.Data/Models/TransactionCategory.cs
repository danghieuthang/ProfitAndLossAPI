using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("TransactionCategories")]
    public class TransactionCategory : BaseEntity<Guid>
    {
        [MaxLength(255)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("ReceiptType")]
        public Guid ReceiptTypeId { get; set; }

        public virtual ReceiptType ReceiptType { get; set; }

        [ForeignKey("Account")]
        public Guid? AccountId { get; set; }

        public Account Account { get; set; }

        public bool IsDebit { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}

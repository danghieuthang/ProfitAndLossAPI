using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("ReceiptTypes")]
    public class ReceiptType : BaseEntity<Guid>
    {
        public ReceiptType()
        {
            
        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        public bool IsDebit { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }

        public virtual ICollection<TransactionCategory> TransactionCategories { get; set; }
    }
}

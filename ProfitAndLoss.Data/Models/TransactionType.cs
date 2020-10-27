using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("TransactionTypes")]
    public class TransactionType : BaseEntity<Guid>
    {
        public TransactionType()
        {
            
        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        public bool IsDebit { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

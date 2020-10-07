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

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("TransactionTypes")]
    public class TransactionType
    {
        public TransactionType()
        {
            Actived = false;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Actived { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

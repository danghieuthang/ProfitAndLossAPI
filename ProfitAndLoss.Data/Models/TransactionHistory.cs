using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("TransactionHistories")]
    public class TransactionHistory : BaseEntity<Guid>
    {
        public TransactionHistory()
        {

        }

        [MaxLength(2000)]
        public string Message { get; set; }

        public int Status { get; set; }

        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }
    }
}

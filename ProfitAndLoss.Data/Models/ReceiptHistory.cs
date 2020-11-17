using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("ReceiptHistories")]
    public class ReceiptHistory : BaseEntity<Guid>
    {
        public ReceiptHistory()
        {

        }

        [MaxLength(2000)]
        public string Message { get; set; }

        public int Status { get; set; }

        [ForeignKey("Receipt")]
        public Guid ReceiptId { get; set; }

        public Receipt Receipt { get; set; }
    }
}

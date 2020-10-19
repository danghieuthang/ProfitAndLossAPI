using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("ReceiptTypes")]
    public class ReceiptType: BaseEntity<Guid>
    {
        public ReceiptType()
        {
                
        }

        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}

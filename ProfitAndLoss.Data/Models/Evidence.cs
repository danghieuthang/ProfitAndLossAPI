using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Evidences")]
    public class Evidence : BaseEntity<Guid>
    {
        public Evidence()
        {

        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ImgUrl { get; set; }

        [ForeignKey("Receipt")]
        public Guid ReceiptId { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}

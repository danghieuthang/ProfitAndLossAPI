using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Receipts")]
    public class Receipt : BaseEntity<Guid>
    {
        public Receipt()
        {
            Evidences = new HashSet<Evidence>();
        }
        public string Code { get; set; }
        public string Description { get; set; }

        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<Evidence> Evidences { get; set; }

    }
}

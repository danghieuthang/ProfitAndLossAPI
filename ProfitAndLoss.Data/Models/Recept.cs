using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Recepts")]
    public class Recept
    {
        public Recept()
        {

        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }

        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}

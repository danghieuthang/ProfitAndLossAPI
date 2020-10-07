using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Recepts")]
    public class Recept : BaseEntity<Guid>
    {
        public Recept()
        {

        }

        [ForeignKey("Member")]
        public Guid CreateMemberId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Evidence> Evidences { get; set; }

    }
}

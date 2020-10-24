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


        public virtual ICollection<Evidence> Evidences { get; set; }

    }
}

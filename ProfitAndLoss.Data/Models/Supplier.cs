using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Suppliers")]
    public class Supplier:BaseEntity<Guid>
    {
        public Supplier()
        {

        }

        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }

    }
}

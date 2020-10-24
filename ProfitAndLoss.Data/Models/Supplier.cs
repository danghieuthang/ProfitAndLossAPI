using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Suppliers")]
    public class Supplier : BaseEntity<Guid>
    {
        public Supplier()
        {

        }

        [MaxLength(255)]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}

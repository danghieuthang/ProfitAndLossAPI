using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Brands")]
    public class Brand
    {
        public Brand()
        {
            Actived = false;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Actived { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<AccountingPeriod> AccountingPeriods { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}

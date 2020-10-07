using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Brands")]
    public class Brand : BaseEntity<Guid>
    {
        public Brand()
        {

        }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<AccountingPeriod> AccountingPeriods { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}

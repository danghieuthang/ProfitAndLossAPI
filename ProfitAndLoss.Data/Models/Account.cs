using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Accounts")]
    public class Account : BaseEntity<Guid>
    {
        public Account()
        {

        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        public string Description { get; set; }

        [DefaultValue(0)]
        public double Balance { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual ICollection<StoreAccount> StoreAccounts { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("StoreAccounts")]
    public class StoreAccount : BaseEntity<Guid>
    {
        public StoreAccount()
        {

        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        public string Description { get; set; }

        public double Balance { get; set; }

        [ForeignKey("Store")]
        public Guid StoreId { get; set; }

        public virtual Store Store { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}

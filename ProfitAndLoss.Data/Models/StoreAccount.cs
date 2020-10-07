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

        public decimal Balance { get; set; }

      
        public Guid StoreId { get; set; }

        public Store Store { get; set; }

     
        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}

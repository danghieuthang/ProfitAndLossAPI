using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Stores")]
    public class Store : BaseEntity<Guid>
    {
        public Store()
        {
            Actived = true;
            Transactions = new HashSet<Transaction>();
            MemberStores = new HashSet<MemberStore>();
            AccountingPeriodInStores = new HashSet<AccountingPeriodInStore>();
        }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [ForeignKey("StoreAccount")]
        public Guid StoreAccountId { get; set; }

        public StoreAccount StoreAccount { get; set; }

        public virtual ICollection<MemberStore> MemberStores { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<AccountingPeriodInStore> AccountingPeriodInStores { get; set; }
    }
}

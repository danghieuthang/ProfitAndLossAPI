using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Stores")]
    public class Store
    {
        public Store()
        {
            Actived = false;
            Transactions = new HashSet<Transaction>();
            MemberStores = new HashSet<MemberStore>();
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Actived { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public virtual ICollection<MemberStore> MemberStores { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<StoreAccount> StoreAccounts { get; set; }

        public virtual ICollection<AccountingPeriodDetail> AccountPeriodDetails { get; set; }

        public virtual ICollection<Recept> Recepts { get; set; }
    }
}

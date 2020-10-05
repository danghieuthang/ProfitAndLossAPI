using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("AccountingPeriodDetail")]
    public class AccountingPeriodDetail
    {
        public AccountingPeriodDetail()
        {

        }
        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Actived { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime ClosedDate { get; set; }

        [ForeignKey("Store")]
        public Guid StoreId { get; set; }

        [ForeignKey("AccountingPeriod")]
        public Guid AccountingPeriodId { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }
        
        public string Description { get; set; }

        public int Status { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }

    }
}

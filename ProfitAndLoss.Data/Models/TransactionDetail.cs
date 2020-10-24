using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("TransactionDetails")]
    public class TransactionDetail : BaseEntity<Guid>
    {
        public TransactionDetail()
        {

        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public double Balance { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }

        [ForeignKey("TransactionCategory")]
        public Guid TransactionCategoryId { get; set; }

        public virtual TransactionCategory TransactionCategory { get; set; }

        [ForeignKey("AccountingPeriodInStore")]
        public Guid AccountingPeriodInStoreId { get; set; }

        public virtual AccountingPeriodInStore AccountingPeriodInStore { get; set; }

    }
}

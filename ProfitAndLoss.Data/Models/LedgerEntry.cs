using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("LedgerEntries")]
    public class LedgerEntry
    {
        public LedgerEntry()
        {

        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public decimal Balance { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [ForeignKey("AccountingPeriod")]
        public Guid AccountingPeriodId { get; set; }

    }
}

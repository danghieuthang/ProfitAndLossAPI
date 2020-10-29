using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("AccountingPeriods")]
    public class AccountingPeriod : BaseEntity<Guid>
    {
        public AccountingPeriod()
        {
            AccountingPeriodInStores = new HashSet<AccountingPeriodInStore>();
            Feedbacks = new HashSet<Feedback>();//
        }

        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public virtual ICollection<AccountingPeriodInStore> AccountingPeriodInStores { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

    }
}

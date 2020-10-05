using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("AccountingPeriods")]
    public class AccountingPeriod
    {
        public AccountingPeriod()
        {
            AccountPeriodDetails = new HashSet<AccountingPeriodDetail>();
            Feedbacks = new HashSet<Feedback>();//
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public bool Actived { get; set; }
        public virtual ICollection<AccountingPeriodDetail> AccountPeriodDetails { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        public Feedback()
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

        public string Description { get; set; }

        [ForeignKey("AccountingPeriod")]
        public Guid AccountingPeriodId { get; set; }

        [ForeignKey("Member")]
        public Guid MemberId { get; set; }


    }
}

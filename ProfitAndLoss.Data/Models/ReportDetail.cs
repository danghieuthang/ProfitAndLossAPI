using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("ReportDetails")]
    public class ReportDetail
    {
        public ReportDetail()
        {
            
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [DefaultValue(0)]
        public int Percent { get; set; }

        public decimal Balance { get; set; }

        public int Status { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [ForeignKey("AccountingPeriod")]
        public Guid AccountingPeriodId { get; set; }

        public bool Actived { get; set; }



    }
}

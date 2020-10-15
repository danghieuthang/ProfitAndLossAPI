using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class FeedbackCreateModel : BaseCreateModel<Feedback>
    {
        public FeedbackCreateModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public Guid AccountingPeriodId { get; set; }
    }

    public class FeedbackUpdateModel : BaseUpdateModel<Feedback>
    {
        public FeedbackUpdateModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class FeedbacSearchModel : BaseSearchModel<Feedback>
    {
        public FeedbacSearchModel()
        {

        }
    }
}

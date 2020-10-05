using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateFeedbackModel : BaseCreateModel<Feedback>
    {
        public RequestCreateFeedbackModel()
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

    public class RequestUpdateFeedbackModel : BaseUpdateModel<Feedback>
    {
        public RequestUpdateFeedbackModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class RequestSearchFeedbackModel : BaseSearchModel<Feedback>
    {
        public RequestSearchFeedbackModel()
        {

        }
    }
}

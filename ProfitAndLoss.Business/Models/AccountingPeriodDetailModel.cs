using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateAccountingPeriodDetailModel : BaseCreateModel<AccountingPeriodDetail>
    {
        public RequestCreateAccountingPeriodDetailModel()
        {

        }

        public bool Actived { get; set; }

        [Required]
        public DateTime StartedDate { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Decription { get; set; }

        public int Status { get; set; }

    }

    public class RequestUpdateAccountingPeriodDetailModel : BaseUpdateModel<AccountingPeriodDetail>
    {
        public RequestUpdateAccountingPeriodDetailModel()
        {

        }

        public bool Actived { get; set; }

        [Required]
        public DateTime StartedDate { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Decription { get; set; }

        public int Status { get; set; }
    }

    public class RequestSearchAccountingPeriodDetailModel : BaseSearchModel<AccountingPeriodDetail>
    {
        public RequestSearchAccountingPeriodDetailModel()
        {

        }
    }
}

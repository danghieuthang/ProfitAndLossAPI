using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateAccountingPeriodModel : BaseCreateModel<AccountingPeriod>
    {
        public RequestCreateAccountingPeriodModel()
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

    public class RequestUpdateAccountingPeriodModel : BaseUpdateModel<AccountingPeriod>
    {
        public RequestUpdateAccountingPeriodModel()
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

    public class RequestSearchAccountingPeriodModel : BaseSearchModel<AccountingPeriod>
    {
        public RequestSearchAccountingPeriodModel()
        {

        }
    }
}

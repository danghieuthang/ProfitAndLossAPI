using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class AccountingPeriodCreateModel : BaseCreateModel<AccountingPeriod>
    {
        public AccountingPeriodCreateModel()
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

    public class AccountingPeriodUpdateModel : BaseUpdateModel<AccountingPeriod>
    {
        public AccountingPeriodUpdateModel()
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

    public class AccountingPeriodSearchModel : BaseSearchModel<AccountingPeriod>
    {
        public AccountingPeriodSearchModel()
        {

        }
    }
}

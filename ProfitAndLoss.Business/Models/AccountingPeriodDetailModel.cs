using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class AccountingPeriodDetailCreateModel : BaseCreateModel<AccountingPeriodDetail>
    {
        public AccountingPeriodDetailCreateModel()
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

    public class AccountingPeriodDetailUpdateModel : BaseUpdateModel<AccountingPeriodDetail>
    {
        public AccountingPeriodDetailUpdateModel()
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

    public class AccountingPeriodDetailSearchModel : BaseSearchModel<AccountingPeriodDetail>
    {
        public AccountingPeriodDetailSearchModel()
        {

        }
    }
}

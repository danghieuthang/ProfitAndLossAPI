using Newtonsoft.Json;
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
            BrandId = new Guid("05FE5BBA-65AD-4B71-A5DD-08D878376F22");
            Status = AccountingPeriodStatus.OPEN;
        }

        public DateTime StartDate { get; set; }

        public DateTime CloseDate { get; set; }

        [JsonIgnore]
        public Guid BrandId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }

    public class AccountingPeriodUpdateModel : BaseUpdateModel<AccountingPeriod>
    {
        public AccountingPeriodUpdateModel()
        {
            BrandId = new Guid("05FE5BBA-65AD-4B71-A5DD-08D878376F22");

        }

        public Guid BrandId { get; set; }

        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CloseDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class AccountingPeriodSearchModel : BaseSearchModel<AccountingPeriod>
    {
        public AccountingPeriodSearchModel()
        {

        }
    }

    public class AccountingPeriodViewModel : BaseViewModel<AccountingPeriod>
    {
        public AccountingPeriodViewModel()
        {

        }

        public DateTime StartDate { get; set; }

        public DateTime CloseDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

    }
}

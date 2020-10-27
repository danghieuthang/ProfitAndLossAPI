﻿using Newtonsoft.Json;
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

        }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("close_date")]
        public DateTime CloseDate { get; set; }

        [JsonProperty("brandId")]
        [JsonIgnore]
        public Guid BrandId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
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

    }
}

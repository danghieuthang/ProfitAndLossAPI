using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class DashboardSearchModel
    {
        public DashboardSearchModel()
        {

        }

        [FromQuery(Name = "store-id")]
        public Guid? StoreId { get; set; }

        [FromQuery(Name = "accounting-period-id")]
        public Guid? AccountingPeriodId { get; set; }
    }

    public class DashboardPieViewModel
    {
        public DashboardPieViewModel()
        {

        }

        [JsonIgnore]
        public Guid TransactionCategoryId { get; set; }

        [JsonProperty("name")]
        public string TransactionCategoryName { get; set; }

        [JsonProperty("balance")]
        public double TotalBalance { get; set; }
    }
}

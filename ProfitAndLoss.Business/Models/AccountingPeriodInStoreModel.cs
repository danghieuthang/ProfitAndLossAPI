using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class AccountingPeriodInStoreCreateModel : BaseCreateModel<AccountingPeriodInStore>
    {
        public AccountingPeriodInStoreCreateModel()
        {

        }

        public DateTime StartDate { get; set; }

        public DateTime CloseDate { get; set; }

        public Guid StoreId { get; set; }

        public Guid AccountingPeriodId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }

    public class AccountingPeriodInStoreUpdateModel : BaseUpdateModel<AccountingPeriodInStore>
    {
        public AccountingPeriodInStoreUpdateModel()
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

        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class AccountingPeriodInStoreSearchModel : BaseSearchModel<AccountingPeriodInStore>
    {
        public AccountingPeriodInStoreSearchModel()
        {

        }
    }
}

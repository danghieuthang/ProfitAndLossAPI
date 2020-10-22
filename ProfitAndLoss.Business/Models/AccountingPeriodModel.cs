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

using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class StoreCreateModel : BaseCreateModel<Store>
    {
        public StoreCreateModel()
        {

        }

        [JsonProperty("brand-id")]
        public Guid BrandId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class StoreUpdateModel : BaseUpdateModel<Store>
    {
        public StoreUpdateModel()
        {

        }

        public bool Actived { get; set; }
    }

    public class StoreSearchModel : BaseSearchModel<Store>
    {
        public StoreSearchModel()
        {

        }

        public Guid BrandId { get; set; }
    }
    public class StoreViewModel : BaseViewModel<Store>
    {
        public StoreViewModel()
        {

        }

        public string Code { get; set; }

        public string Name { get; set; }

        public BrandViewModel Brand { get; set; }
    }
}

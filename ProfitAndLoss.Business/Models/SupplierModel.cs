using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class SupplierCreateModel : BaseCreateModel<Supplier>
    {
        public SupplierCreateModel()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class SupplierSearchModel : BaseSearchModel<Supplier>
    {

    }

    public class SupplierViewModel : BaseViewModel<Supplier>
    {
        public SupplierViewModel()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

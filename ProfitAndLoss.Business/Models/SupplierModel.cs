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

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

    }

    public class SupplierSearchModel : BaseSearchModel<Supplier>
    {

    }

    public class SupplierViewModel : BaseViewModel<Supplier>
    {
        public SupplierViewModel()
        {

        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}

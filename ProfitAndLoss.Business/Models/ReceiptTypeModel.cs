using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class ReceiptTypeCreateModel: BaseCreateModel<ReceiptType>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
    public class ReceiptTypeUpdateModel
    {
    }
    public class ReceiptTypeSearchModel
    {
    }
}

using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using System;

namespace ProfitAndLoss.Business.Models
{
    public class ReceiptCreateModel : BaseCreateModel<Receipt>
    {
        public ReceiptCreateModel()
        {

        }

        [JsonProperty("store-id")]
        public Guid StoreId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class ReceiptUpdateModel : BaseUpdateModel<Receipt>
    {
        public ReceiptUpdateModel()
        {

        }
    }

    public class ReceiptSearchModel : BaseSearchModel<Receipt>
    {
        public ReceiptSearchModel()
        {

        }

    }
}

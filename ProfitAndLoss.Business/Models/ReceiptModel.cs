using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using System;

namespace ProfitAndLoss.Business.Models
{
    public class ReceiptCreateModel : BaseCreateModel<Receipt>
    {
        public ReceiptCreateModel()
        {
            Status = CommonConstants.ReceiptStatus.CREATED;
        }

        [JsonProperty("store-id")]
        public Guid StoreId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type-id")]
        public Guid TypeId { get; set; }

        [JsonProperty("supplier-id")]
        public Guid SupplierId { get; set; }

        [JsonIgnore]
        public int Status { get; set; }

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

    public class ReceiptViewModel
    {
        public ReceiptViewModel()
        {

        }

        public string Description { get; set; }


        public string Type { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }
    }
}

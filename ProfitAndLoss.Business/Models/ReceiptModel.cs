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

        [JsonProperty("store_id")]
        public Guid StoreId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("type_id")]
        public Guid TypeId { get; set; }

        [JsonProperty("supplier_id")]
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

    public class ReceiptViewModel:BaseViewModel<Receipt>
    {
        public ReceiptViewModel()
        {

        }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }


        [JsonProperty("supplier")]
        //public string SupplierId { get; set; }
        public SupplierViewModel Supplier { get; set; }

        [JsonProperty("store")]
        //public string StoreId { get; set; }
        public StoreViewModel Store { get; set; }

        [JsonProperty("modified_date")]
        public DateTime ModifiedDate { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}

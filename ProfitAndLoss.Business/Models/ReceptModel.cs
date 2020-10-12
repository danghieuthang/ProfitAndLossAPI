using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using System;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateReceptModel : BaseCreateModel<Recept>
    {
        public RequestCreateReceptModel()
        {

        }

        [JsonProperty("store-id")]
        public Guid StoreId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class RequestUpdateReceptModel : BaseUpdateModel<Recept>
    {
        public RequestUpdateReceptModel()
        {

        }
    }

    public class RequestSearchReceptModel : BaseSearchModel<Recept>
    {
        public RequestSearchReceptModel()
        {

        }

    }
}

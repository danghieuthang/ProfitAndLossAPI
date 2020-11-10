using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using System;
using System.Runtime.Serialization;

namespace ProfitAndLoss.Business.Models
{
    public class EvidenceCreateModel : BaseCreateModel<Evidence>
    {
        public EvidenceCreateModel()
        {

        }

        [JsonIgnore]
        [FromForm(Name ="image")]
        public IFormFile Image { get; set; }
        [FromForm(Name = "receipt-id")]
        public Guid ReceiptId { get; set; }

        [FromForm(Name = "name")]
        public string Name { get; set; }

        [FromForm(Name ="img-url")]
        public string ImgUrl { get; set; }

    }

    public class EvidenceUpdateModel : BaseUpdateModel<Evidence>
    {
        public EvidenceUpdateModel()
        {

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Actived { get; set; }

        public string ImUrl { get; set; }
    }

    public class EvidenceSearchModel : BaseSearchModel<Evidence>
    {
        public EvidenceSearchModel()
        {

        }
        [FromQuery(Name = "name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [FromQuery(Name = "receipt-id")]
        [JsonProperty("receipt-id")]
        public Guid ReceiptId { get; set; }

    }

    public class EvidenceViewModel: BaseViewModel<Evidence>
    {
        public EvidenceViewModel()
        {

        }
        public string Name { get; set; }

        public string ImgUrl { get; set; }
    }
}

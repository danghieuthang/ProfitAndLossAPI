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

        [FromForm(Name = "image")]
        public IFormFile Image { get; set; }

        [FromForm(Name = "name")]
        public string Name { get; set; }

        [FromForm(Name = "description")]
        public string Description { get; set; }

        [FromForm(Name = "img_url")]

        public string ImgUrl { get; set; }

        [FromForm(Name = "receipt_id")]
        public Guid ReceiptId { get; set; }
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

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("receipt_id")]
        public Guid ReceiptId { get; set; }

    }
}

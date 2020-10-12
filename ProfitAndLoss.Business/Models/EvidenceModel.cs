using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using System;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateEvidenceModel : BaseCreateModel<Evidence>
    {
        public RequestCreateEvidenceModel()
        {

        }

        [JsonProperty("image")]
        public IFormFile Image { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonIgnore]
        public string ImgUrl { get; set; }

        [JsonProperty("recept-id")]
        public Guid ReceptId { get; set; }
    }

    public class RequestUpdateEvidenceModel : BaseUpdateModel<Evidence>
    {
        public RequestUpdateEvidenceModel()
        {

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Actived { get; set; }

        public string ImUrl { get; set; }
    }

    public class RequestSearchEvidenceModel : BaseSearchModel<Evidence>
    {
        public RequestSearchEvidenceModel()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recept-id")]
        public Guid ReceptId { get; set; }

    }
}

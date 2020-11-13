using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.ComponentModel;

namespace ProfitAndLoss.Business.Models
{
    public class BaseSearchModel<T> : Mapping<T>
    {
        public BaseSearchModel()
        {

        }

        [FromQuery(Name = "from-date")]
        [JsonProperty("from-date")]
        public DateTime? FromDate { get; set; }

        [FromQuery(Name = "to-date")]
        [JsonProperty("to-date")]
        public DateTime? ToDate { get; set; }

        [FromQuery(Name = "page")]
        [JsonProperty("page")]
        public int Page { get; set; }

        [FromQuery(Name = "page-size")]
        [JsonProperty("page-size")]
        public int PageSize { get; set; }

        [FromQuery(Name = "sort-by")]
        [JsonProperty("sort-by")]
        public string SortBy { get; set; }
    }

    public class BaseUpdateModel<T> : Mapping<T>
    {
        public BaseUpdateModel()
        {
            ModifiedDate = DateTime.Now;
        }
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }

        public bool Actived { get; set; }
    }

    public class BaseCreateModel<T> : Mapping<T>
    {
        public BaseCreateModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = CreatedDate;
            Actived = true;
        }

        [JsonIgnore]
        [FromForm(Name = "created-date")]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        [FromForm(Name = "modified-date")]
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        [FromForm(Name = "actived")]
        public bool Actived { get; set; }
    }

    public class BaseExportModel<T> : Mapping<T>
    {
        public BaseExportModel()
        {

        }
    }

    public class BaseViewModel<T> : Mapping<T>
    {
        public BaseViewModel()
        {

        }

        public Guid Id { get; set; }
    }

}

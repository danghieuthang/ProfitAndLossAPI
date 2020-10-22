using Newtonsoft.Json;
using System;

namespace ProfitAndLoss.Business.Models
{
    public class BaseSearchModel<T> : Mapping<T>
    {
        public BaseSearchModel()
        {

        }

        [JsonProperty("created_date_from")]
        public DateTime TransactionDateFrom { get; set; }

        [JsonProperty("created_date_to")]
        public DateTime TransactionDateTo { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("sort_by")]
        public string SortBy { get; set; }
    }

    public class BaseUpdateModel<T> : Mapping<T>
    {
        public BaseUpdateModel()
        {
            ModifiedDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }

    public class BaseCreateModel<T> : Mapping<T>
    {
        public BaseCreateModel()
        {
            CreatedDate = DateTime.Now;
            Actived = true;
        }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
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

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

}

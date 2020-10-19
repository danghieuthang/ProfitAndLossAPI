using Newtonsoft.Json;
using System;

namespace ProfitAndLoss.Business.Models
{
    public class BaseSearchModel<T> : Mapping<T>
    {
        public BaseSearchModel()
        {

        }

        public DateTime TransactionDateFrom { get; set; }

        public DateTime TransactionDateTo { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

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

}

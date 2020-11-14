using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class TransactionTypeCreateModel : BaseCreateModel<TransactionType>
    {
        public TransactionTypeCreateModel()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isdebit")]
        public bool IsDebit { get; set; }

    }

    public class TransactionTypeUpdateModel : BaseUpdateModel<TransactionType>
    {
        public TransactionTypeUpdateModel()
        {

        }

        public bool Actived { get; set; }
    }

    public class TransactionTypeSearchModel : BaseSearchModel<TransactionType>
    {
        public TransactionTypeSearchModel()
        {

        }
    }

    public class TransactionTypeViewModel: BaseViewModel<TransactionType>
    {
        public TransactionTypeViewModel()
        {

        }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsDebit { get; set; }
    }
}

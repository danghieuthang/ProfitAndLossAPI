using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class ReceiptTypeCreateModel : BaseCreateModel<ReceiptType>
    {
        public ReceiptTypeCreateModel()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isdebit")]
        public bool IsDebit { get; set; }

    }

    public class ReceiptTypeUpdateModel : BaseUpdateModel<ReceiptType>
    {
        public ReceiptTypeUpdateModel()
        {

        }

    }

    public class ReceiptTypeSearchModel : BaseSearchModel<ReceiptType>
    {
        public ReceiptTypeSearchModel()
        {

        }
    }

    public class ReceiptTypeViewModel: BaseViewModel<ReceiptType>
    {
        public ReceiptTypeViewModel()
        {

        }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsDebit { get; set; }
    }
}

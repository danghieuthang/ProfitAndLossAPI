using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using System;
using System.Collections.Generic;

namespace ProfitAndLoss.Business.Models
{
    public class ReceiptCreateModel : BaseCreateModel<Receipt>
    {
        public ReceiptCreateModel()
        {
    
        }
        [JsonIgnore]
        public Guid? TransactionId { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public int? Status { get; set; }
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

        public string Description { get; set; }

        public List<EvidenceViewModel> Evidences { get; set; }
    }

}

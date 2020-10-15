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

        public bool Actived { get; set; }

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
}

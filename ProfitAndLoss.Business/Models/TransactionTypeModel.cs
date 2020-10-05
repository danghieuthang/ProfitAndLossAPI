using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateTransactionTypeModel : BaseCreateModel<TransactionType>
    {
        public RequestCreateTransactionTypeModel()
        {

        }

        public bool Actived { get; set; }

    }

    public class RequestUpdateTransactionTypeModel : BaseUpdateModel<TransactionType>
    {
        public RequestUpdateTransactionTypeModel()
        {

        }

        public bool Actived { get; set; }
    }

    public class RequestSearchTransactionTypeModel : BaseSearchModel<TransactionType>
    {
        public RequestSearchTransactionTypeModel()
        {

        }
    }
}

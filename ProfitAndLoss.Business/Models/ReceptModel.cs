using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateReceptModel : BaseCreateModel<Recept>
    {
        public RequestCreateReceptModel()
        {

        }

    }

    public class RequestUpdateReceptModel : BaseUpdateModel<Recept>
    {
        public RequestUpdateReceptModel()
        {

        }
    }

    public class RequestSearchReceptModel : BaseSearchModel<Recept>
    {
        public RequestSearchReceptModel()
        {

        }

        public Guid TransactionId { get; set; }

    }
}

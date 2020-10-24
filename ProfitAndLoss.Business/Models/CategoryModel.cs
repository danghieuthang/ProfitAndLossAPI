using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class CateogoryCreateModel : BaseCreateModel<TransactionCategory>
    {
        public CateogoryCreateModel()
        {

        }

        public bool Actived { get; set; }

        public Guid ParentId { get; set; }

    }

    public class TransactionCategoryUpdateModel : BaseUpdateModel<TransactionCategory>
    {
        public TransactionCategoryUpdateModel()
        {

        }

        public bool Actived { get; set; }

        public Guid ParentId { get; set; }
    }

    public class TransactionCategorySearchModel : BaseSearchModel<TransactionCategory>
    {
        public TransactionCategorySearchModel()
        {

        }
    }
}

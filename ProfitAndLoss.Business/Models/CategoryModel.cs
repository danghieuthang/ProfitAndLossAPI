using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateCategoryModel : BaseCreateModel<Category>
    {
        public RequestCreateCategoryModel()
        {

        }

        public bool Actived { get; set; }

        public Guid ParentId { get; set; }

    }

    public class RequestUpdateCategoryModel : BaseUpdateModel<Category>
    {
        public RequestUpdateCategoryModel()
        {

        }

        public bool Actived { get; set; }

        public Guid ParentId { get; set; }
    }

    public class RequestSearchCategoryModel : BaseSearchModel<Category>
    {
        public RequestSearchCategoryModel()
        {

        }
    }
}

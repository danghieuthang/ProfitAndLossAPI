using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class CateogoryCreateModel : BaseCreateModel<Category>
    {
        public CateogoryCreateModel()
        {

        }

        public bool Actived { get; set; }

        public Guid ParentId { get; set; }

    }

    public class CategoryUpdateModel : BaseUpdateModel<Category>
    {
        public CategoryUpdateModel()
        {

        }

        public bool Actived { get; set; }

        public Guid ParentId { get; set; }
    }

    public class CategorySearchModel : BaseSearchModel<Category>
    {
        public CategorySearchModel()
        {

        }
    }
}

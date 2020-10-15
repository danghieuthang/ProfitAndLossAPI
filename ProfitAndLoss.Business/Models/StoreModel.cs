using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class StoreCreateModel : BaseCreateModel<Store>
    {
        public StoreCreateModel()
        {

        }

        public Guid BrandId { get; set; }
        public bool Actived { get; set; }

    }

    public class StoreUpdateModel : BaseUpdateModel<Store>
    {
        public StoreUpdateModel()
        {

        }

        public bool Actived { get; set; }
    }

    public class StoreSearchModel : BaseSearchModel<Store>
    {
        public StoreSearchModel()
        {

        }

        public Guid BrandId { get; set; }
    }
}

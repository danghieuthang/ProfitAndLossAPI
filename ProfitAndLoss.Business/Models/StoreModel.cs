using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateStoreModel : BaseCreateModel<Store>
    {
        public RequestCreateStoreModel()
        {

        }

        
        public bool Actived { get; set; }

    }

    public class RequestUpdateStoreModel : BaseUpdateModel<Store>
    {
        public RequestUpdateStoreModel()
        {

        }

        public bool Actived { get; set; }
    }

    public class RequestSearchStoreModel : BaseSearchModel<Store>
    {
        public RequestSearchStoreModel()
        {

        }

        public Guid BrandId { get; set; }
    }
}

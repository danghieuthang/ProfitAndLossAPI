using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateBrandModel : BaseCreateModel<Brand>
    {
        public RequestCreateBrandModel()
        {

        }
        public bool Actived { get; set; }

    }

    public class RequestUpdateBrandModel : BaseUpdateModel<Brand>
    {
        public RequestUpdateBrandModel()
        {

        }
        public bool Actived { get; set; }
    }

    public class RequestSearchBrandModel : BaseSearchModel<Brand>
    {
        public bool Actived { get; set; }
    }
}

using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateStoreAccountModel : BaseCreateModel<StoreAccount>
    {
        public RequestCreateStoreAccountModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public Guid StoreId { get; set; }

    }

    public class RequestUpdateStoreAccountModel : BaseUpdateModel<StoreAccount>
    {
        public RequestUpdateStoreAccountModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public Guid StoreId { get; set; }
    }

    public class RequestSearchStoreAccountModel : BaseSearchModel<StoreAccount>
    {

    }
}

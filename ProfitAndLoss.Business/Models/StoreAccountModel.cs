using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class StoreAccountCreateModel : BaseCreateModel<StoreAccount>
    {
        public StoreAccountCreateModel()
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

    public class StoreAccountUpdateModel : BaseUpdateModel<StoreAccount>
    {
        public StoreAccountUpdateModel()
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

    public class StoreAccountSearchModel : BaseSearchModel<StoreAccount>
    {

    }
}

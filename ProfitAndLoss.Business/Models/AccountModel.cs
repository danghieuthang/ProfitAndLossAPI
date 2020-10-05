using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateAccountModel : BaseCreateModel<Account>
    {
        public RequestCreateAccountModel()
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

    }

    public class RequestUpdateAccountModel : BaseUpdateModel<Account>
    {
        public RequestUpdateAccountModel()
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

    }

    public class RequestSearchAccountModel : BaseSearchModel<Account>
    {
        public RequestSearchAccountModel()
        {

        }
    }
}

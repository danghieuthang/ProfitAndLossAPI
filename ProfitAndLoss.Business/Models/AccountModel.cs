using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class AccountCreateModel : BaseCreateModel<Account>
    {
        public AccountCreateModel()
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

    public class AccountUpdateModel : BaseUpdateModel<Account>
    {
        public AccountUpdateModel()
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

    public class AccountSearchModel : BaseSearchModel<Account>
    {
        public AccountSearchModel()
        {

        }
    }
}

using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class TransactionDetailCreateModel : BaseCreateModel<TransactionDetail>
    {
        public TransactionDetailCreateModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Decription { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public Guid TransactionCategoryId { get; set; }
    }

    public class TransactionDetailUpdateModel : BaseUpdateModel<TransactionDetail>
    {
        public TransactionDetailUpdateModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Decription { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public Guid TransactionCategoryId { get; set; }
    }

    public class TransactionDetailSearchModel : BaseSearchModel<TransactionDetail>
    {
        public TransactionDetailSearchModel()
        {

        }
    }
}

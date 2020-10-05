using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateTransactionDetailModel : BaseCreateModel<TransactionDetail>
    {
        public RequestCreateTransactionDetailModel()
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
        public Guid CategoryId { get; set; }
    }

    public class RequestUpdateTransactionDetailModel : BaseUpdateModel<TransactionDetail>
    {
        public RequestUpdateTransactionDetailModel()
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
        public Guid CategoryId { get; set; }
    }

    public class RequestSearchTransactionDetailModel : BaseSearchModel<TransactionDetail>
    {
        public RequestSearchTransactionDetailModel()
        {

        }
    }
}

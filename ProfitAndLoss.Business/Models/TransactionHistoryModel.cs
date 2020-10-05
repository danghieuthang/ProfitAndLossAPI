using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateTransactionHistoryModel : BaseCreateModel<TransactionHistory>
    {
        public RequestCreateTransactionHistoryModel()
        {

        }

        [Required]
        public Guid TransactionId { get; set; }

        public bool Actived { get; set; }

        [Required]
        public string Message { get; set; }

        public int Status { get; set; }

    }

    public class RequestUpdateTransactionHistoryModel : BaseUpdateModel<TransactionHistory>
    {
        public RequestUpdateTransactionHistoryModel()
        {

        }

        public bool Actived { get; set; }

        [Required]
        public string Message { get; set; }

        public int Status { get; set; }
    }

    public class RequestSearchTransactionHistoryModel : BaseSearchModel<TransactionHistory>
    {
        public RequestSearchTransactionHistoryModel()
        {

        }
    }
}

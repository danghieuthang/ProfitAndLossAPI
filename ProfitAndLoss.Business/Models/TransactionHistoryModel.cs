using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class TransactionHistoryCreateModel : BaseCreateModel<TransactionHistory>
    {
        public TransactionHistoryCreateModel()
        {

        }

        [Required]
        public Guid TransactionId { get; set; }

        public bool Actived { get; set; }

        [Required]
        public string Message { get; set; }

        public int Status { get; set; }

    }

    public class TransactionHistoryUpdateModel : BaseUpdateModel<TransactionHistory>
    {
        public TransactionHistoryUpdateModel()
        {

        }

        public bool Actived { get; set; }

        [Required]
        public string Message { get; set; }

        public int Status { get; set; }
    }

    public class TransactionHistorySearchModel : BaseSearchModel<TransactionHistory>
    {
        public TransactionHistorySearchModel()
        {

        }
    }
}

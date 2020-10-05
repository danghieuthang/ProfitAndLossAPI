using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateTransactionModel : BaseCreateModel<Transaction>
    {
        public RequestCreateTransactionModel()
        {

        }

        [Required]
        public Guid CreateMemberId { get; set; }

        public Guid MasterTransactionId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public Decimal Price { get; set; }

        public string NoteMessage { get; set; }
    }

    public class RequestUpdateTransactionModel : BaseUpdateModel<Transaction>
    {
        public RequestUpdateTransactionModel()
        {

        }

        public Guid MasterTransactionId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public Decimal Price { get; set; }

        public string NoteMessage { get; set; }
    }

    public class RequestSearchTransactionModel : BaseSearchModel<Transaction>
    {
        public RequestSearchTransactionModel()
        {

        }
    }
}

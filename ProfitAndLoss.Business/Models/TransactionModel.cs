using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class TransactionCreateModel : BaseCreateModel<Transaction>
    {
        public TransactionCreateModel()
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

    public class TransactionUpdateModel : BaseUpdateModel<Transaction>
    {
        public TransactionUpdateModel()
        {

        }

        public Guid MasterTransactionId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public Decimal Price { get; set; }

        public string NoteMessage { get; set; }
    }

    public class TransactionSearchModel : BaseSearchModel<Transaction>
    {
        public TransactionSearchModel()
        {

        }
    }
}

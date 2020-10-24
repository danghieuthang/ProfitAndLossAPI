using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
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
            Status = CommonConstants.TransactionStatus.NEW; // New
        }

        [Required]
        public Guid CreateMemberId { get; set; }

        [JsonProperty("transaction-type-id")]
        public Guid TransactionTypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("note-message")]
        public string NoteMessage { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
    }

    public class TransactionUpdateModel : BaseUpdateModel<Transaction>
    {
        public TransactionUpdateModel()
        {

        }

    }

    public class TransactionSearchModel : BaseSearchModel<Transaction>
    {
        public TransactionSearchModel()
        {

        }
    }
}

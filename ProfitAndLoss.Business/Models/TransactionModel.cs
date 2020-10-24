using Microsoft.AspNetCore.Mvc;
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
        public Guid CreateMemberId { get; set; }

        public Guid TransactionTypeId { get; set; }
        public Guid StoreId { get; set; }
        public Guid SupplierId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public double Balance { get; set; }

        public string NoteMessage { get; set; }

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

    public class TransactionViewModel: BaseViewModel<Transaction>
    {
        public TransactionViewModel()
        {

        }

        [JsonProperty("member")]
        public MemberViewModel Member { get; set; }

        [JsonProperty("transaction-type")]
        public TransactionTypeViewModel TransactionType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("note-message")]
        public string NoteMessage { get; set; }

        [JsonProperty("store")]
        public StoreViewModel Store { get; set; }

        [JsonProperty("supplier")]
        public SupplierViewModel Supplier { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class TransactionCreateModel : BaseCreateModel<Transaction>
    {
        public TransactionCreateModel()
        {
            Status = (int)TransactionStatus.NEW; // New
        }
        [JsonIgnore]
        public Guid? CreateMemberId { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? SupplierId { get; set; }
        //public string SupplierName { get; set; }
        public ReceiptCreateModel Receipt { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string Code { get; set; }
        public double Balance { get; set; }

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

        public Guid? TransactionTypeId { get; set; }
        public Guid? StoreId { get; set; }
        public int Status { get; set; }
        public string Code { get; set; } 
    }

    public class TransactionViewModel : BaseViewModel<Transaction>
    {
        public TransactionViewModel()
        {

        }

        public MemberViewModel Member { get; set; }

        public TransactionTypeViewModel TransactionType { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public double Balance { get; set; }

        public string NoteMessage { get; set; }

        public StoreViewModel Store { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public Guid ReceiptId { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

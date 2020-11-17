using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public Guid ReceiptId { get; set; }

        [Required]
        public Guid AccountingPeriodId { get; set; }

        [JsonIgnore]
        public Guid AccountingPeriodInStoreId { get; set; }

        [Required]
        public Guid TransactionCategoryId { get; set; }

        public Guid StoreId { get; set; }

    }

    public class TransactionItem : BaseCreateModel<Transaction>
    {
        public TransactionItem()
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

    public class TransactionUpdateModel : BaseUpdateModel<Transaction>
    {
        public TransactionUpdateModel()
        {

        }

        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid AccountingPeriodId { get; set; }

        [JsonIgnore]
        public Guid AccountingPeriodInStoreId { get; set; }

        [Required]
        public Guid StoreId { get; set; }
    }

    public class TransactionSearchModel : BaseSearchModel<Transaction>
    {
        public TransactionSearchModel()
        {

        }

        [FromQuery(Name = "code")]
        public string Code { get; set; }

        [FromQuery(Name = "store-id")]
        public Guid? StoreId { get; set; }

        [FromQuery(Name = "accounting-period-id")]
        public Guid? AccountingPeriodId { get; set; }

        [FromQuery(Name = "transaction-category-id")]
        public Guid? TransactionCategoryId { get; set; }

    }

    public class TransactionViewModel : BaseViewModel<Transaction>
    {
        public TransactionViewModel()
        {

        }

        public string Description { get; set; }

        public double Balance { get; set; }

        public StoreViewModel Store { get; set; }

        public AccountingPeriodViewModel AccountingPeriod { get; set; }

        public TransactionCategoryViewModel TransactionCategory { get; set; }
    }
}

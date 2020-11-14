using Newtonsoft.Json;
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

        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public Guid AccountingPeriodId { get; set; }

        [JsonIgnore]
        public Guid AccountingPeriodInStoreId { get; set; }

        [Required]
        public Guid TransactionCategoryId { get; set; }

        public Guid StoreId { get; set; }

    }

    public class TransactionDetailItem : BaseCreateModel<TransactionDetail>
    {
        public TransactionDetailItem()
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

        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid AccountingPeriodId { get; set; }

        [JsonIgnore]
        public Guid AccountingPeriodInStoreId { get; set; }
        
        [Required]
        public Guid StoreId { get; set; }
    }

    public class TransactionDetailSearchModel : BaseSearchModel<TransactionDetail>
    {
        public TransactionDetailSearchModel()
        {

        }
    }

    public class TransactionDetailViewModel : BaseViewModel<TransactionDetail>
    {
        public TransactionDetailViewModel()
        {

        }

        public string Description { get; set; }

        public double Balance { get; set; }

        public StoreViewModel Store { get; set; }

        public AccountingPeriodViewModel AccountingPeriod { get; set; }

        public TransactionCategoryViewModel TransactionCategory { get; set; }
    }
}

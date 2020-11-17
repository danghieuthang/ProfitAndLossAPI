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
    public class ReceiptCreateModel : BaseCreateModel<Receipt>
    {
        public ReceiptCreateModel()
        {
            Status = (int)ReceiptStatus.NEW; // New
        }
        [JsonIgnore]
        public Guid? CreateMemberId { get; set; }
        public Guid? ReceiptTypeId { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? SupplierId { get; set; }
        //public string SupplierName { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string Code { get; set; }
        public double TotalBalance { get; set; }
        [JsonIgnore]
        public double SubTotal { get { return TotalBalance - (ShippingFee + DiscountValue); } }
        public double ShippingFee { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountValue { get; set; }
        public DateTime TermExport { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }

        public string NoteMessage { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
    }

    public class ReceiptUpdateModel : BaseUpdateModel<Receipt>
    {
        public ReceiptUpdateModel()
        {

        }

    }

    public class ReceiptSearchModel : BaseSearchModel<Receipt>
    {
        public ReceiptSearchModel()
        {

        }

        public Guid? TransactionTypeId { get; set; }
        public Guid? StoreId { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
    }

    public class ReceiptViewModel : BaseViewModel<Receipt>
    {
        public ReceiptViewModel()
        {

        }

        public MemberViewModel Member { get; set; }

        public ReceiptTypeViewModel TransactionType { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public double TotalBalance { get; set; }

        public double SubTotal { get; set; }

        public double ShippingFee { get; set; }

        public double DiscountPercent { get; set; }

        public double DiscountValue { get; set; }

        public string NoteMessage { get; set; }

        public StoreViewModel Store { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

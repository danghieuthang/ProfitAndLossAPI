using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class ReceiptHistoryCreateModel : BaseCreateModel<ReceiptHistory>
    {
        public ReceiptHistoryCreateModel()
        {

        }

        [Required]
        public Guid ReceiptId { get; set; }


        [Required]
        public string Message { get; set; }

        public int Status { get; set; }

    }

    public class ReceiptHistoryUpdateModel : BaseUpdateModel<ReceiptHistory>
    {
        public ReceiptHistoryUpdateModel()
        {

        }


        [Required]
        public string Message { get; set; }

        public int Status { get; set; }
    }

    public class ReceiptHistorySearchModel : BaseSearchModel<ReceiptHistory>
    {
        public ReceiptHistorySearchModel()
        {

        }
    }
}

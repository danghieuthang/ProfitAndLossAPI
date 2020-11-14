using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class ProfitAndLossSearchModel
    {
        public ProfitAndLossSearchModel()
        {

        }

        [FromForm(Name = "store-id")]
        public Guid? StoreId { get; set; }

        [FromForm(Name = "accounting-period-id")]
        public Guid? AccountingPeriodId { get; set; }
    }

    public class ProfitAndLossViewModel
    {
        public ProfitAndLossViewModel()
        {

        }

        public List<ProfitAndLossItemModel> Incomes { get; set; }
        public double GrossProfit { get; set; }
        public List<ProfitAndLossItemModel> Expenses { get; set; }
    }

    public class ProfitAndLossItemModel
    {
        public ProfitAndLossItemModel()
        {

        }

        public string Name { get; set; }

        public double Balance { get; set; }
    }

    public class ExpenseViewModel
    {
        public ExpenseViewModel()
        {

        }

        public string Name { get; set; }

        public double Balance { get; set; }
    }

}

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

        [FromQuery(Name = "store-id")]
        public Guid? StoreId { get; set; }

        [FromQuery(Name = "accounting-period-id")]
        public Guid? AccountingPeriodId { get; set; }
    }

    public class ProfitAndLossViewWebModel
    {
        public ProfitAndLossViewWebModel()
        {

        }

        public List<ProfitAndLossItemModel> Incomes { get; set; }
        public double GrossProfit { get; set; }
        public double CostOfGoodsSold { get; set; }

        public List<ProfitAndLossItemModel> Expenses { get; set; }
    }

    public class ProfitAndLossViewMobileModel
    {
        public ProfitAndLossViewMobileModel()
        {

        }
        public DateTime StartedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public GroupProfitAndLossItemModel Incomes { get; set; }
        public GroupProfitAndLossItemModel Expenses { get; set; }
        public GroupProfitAndLossItemModel CostOfGoodsSold { get; set; }
        public double GrossProfit { get; set; }
        public double NetProfit { get; set; }
    }

    public class GroupProfitAndLossItemModel
    {
        public GroupProfitAndLossItemModel()
        {

        }
        public string Title { get; set; }
        public string EndTitle { get; set; }
        public double TotalAmount { get; set; }
        public List<ProfitAndLossItemModel> ListCategory { get; set; }
    }

    public class ProfitAndLossItemModel
    {
        public ProfitAndLossItemModel()
        {

        }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string Account { get; set; }
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

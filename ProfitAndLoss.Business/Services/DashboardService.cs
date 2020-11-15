using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ProfitAndLoss.Business.Helpers;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Extensions;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IDashboardService : IDisposable
    {
        Task<GenericResult> GetTransaction(DashboardSearchModel model);
        Task<GenericResult> GetRevenuesPie(DashboardSearchModel model);
        Task<GenericResult> GetExpensePie(DashboardSearchModel model);
        Task<GenericResult> GetRevenueExpense(DashboardSearchModel model);
        Task<GenericResult> GetProfitAndLoss(ProfitAndLossSearchModel model);
        Task<byte[]> Export(ProfitAndLossSearchModel model);
        Task<GenericResult> GetProfitAndLossMobile(ProfitAndLossSearchModel model);
    }

    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Color BackGRColor = Color.FromArgb(50, 82, 168);
        private static readonly Color TitleBGRColor = Color.FromArgb(91, 155, 213);
        private static readonly Color YellowBGRColor = Color.FromArgb(255, 255, 0);

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> GetExpensePie(DashboardSearchModel model)
        {
            var result = await GetPieByTranactionCategory(model, false);
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        private async Task<List<DashboardPieViewModel>> GetPieByTranactionCategory(DashboardSearchModel model, bool isDebit)
        {
            if (model.AccountingPeriodId == null)
            {
                model.AccountingPeriodId = _unitOfWork.AccountingPeriodRepository.GetCurrentAccountPeriod().Id;
            }
            var result = _unitOfWork.TransactionCategoryRepository.GetAll(x => x.IsDebit == isDebit)
                .Select(x => new DashboardPieViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalBalance = x.TransactionDetails.Where(t =>
                        (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                        && (t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                        .Sum(t => t.Balance)
                }).Where(x => x.TotalBalance > 0)
                .ToList();

            return result;
        }

        private async Task<List<DashboardPieViewModel>> GetPieByTranactionType(DashboardSearchModel model, bool isDebit)
        {
            var result = _unitOfWork.TransactionCategoryRepository.GetAll(x => x.IsDebit == isDebit)
                .Include(x => x.TransactionType)
                .Where(x => x.IsDebit == x.TransactionType.IsDebit)
                .Select(x => new
                {
                    ID = x.TransactionType.Id,
                    Name = x.TransactionType.Name,
                    TotalBalance = x.TransactionDetails.Where(t =>
                        (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                        && (model.AccountingPeriodId == null || t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                        .Sum(t => t.Balance)
                })
                .Where(x => x.TotalBalance > 0)
                .Distinct()
                .ToList()
                .GroupBy(x => x.ID)
                .Select(g => new DashboardPieViewModel
                {
                    Id = g.Key,
                    Name = g.Select(x => x.Name).FirstOrDefault(),
                    TotalBalance = g.Sum(x => x.TotalBalance)
                })
                .Where(x => x.TotalBalance > 0)
                .ToList();
            return result;
        }

        public async Task<GenericResult> GetRevenuesPie(DashboardSearchModel model)
        {
            var result = await GetPieByTranactionCategory(model, true);
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        public async Task<GenericResult> GetTransaction(DashboardSearchModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<GenericResult> GetRevenueExpense(DashboardSearchModel model)
        {
            // default get current Accounting Period
            if (model.AccountingPeriodId == null)
            {
                model.AccountingPeriodId = _unitOfWork.AccountingPeriodRepository.GetAll(x =>
                        x.StartDate <= DateTime.Now
                        && x.CloseDate >= DateTime.Now
                        ).FirstOrDefault().Id;
            }
            // Query revenues and expense
            var result = _unitOfWork.TransactionDetailRepository.GetAll(x =>
            (model.StoreId == null || x.AccountingPeriodInStore.StoreId == model.StoreId.Value)
            && (model.AccountingPeriodId == x.AccountingPeriodInStore.AccountingPeriodId))
                .Include(x => x.TransactionCategory)
                .ToList()
                .Select(x => new
                {
                    x.Balance,
                    Date = x.CreatedDate.GetMonthYear(),
                    x.TransactionCategory.IsDebit
                })
                .GroupBy(x => x.Date, (key, trans) => new
                {
                    Date = key,
                    Revenue = trans.Where(x => x.IsDebit).Sum(x => x.Balance),
                    Expense = trans.Where(x => !x.IsDebit).Sum(x => x.Balance)
                }).ToList();
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        public async Task<GenericResult> GetProfitAndLoss(ProfitAndLossSearchModel model)
        {
            // get query data
            var result = await QueryProfitAndLossView(model);

            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        /// <summary>
        /// Query Proft and and loss view obj
        /// </summary>
        /// <param name="model">The profit and loss search model</param>
        /// <returns></returns>
        private async Task<ProfitAndLossViewWebModel> QueryProfitAndLossView(ProfitAndLossSearchModel model)
        {
            // Get current accounting period if accounting period id null

            if (model.AccountingPeriodId == null)
            {
                model.AccountingPeriodId = _unitOfWork.AccountingPeriodRepository.GetCurrentAccountPeriod().Id;
            }

            var incomes = QueryListProfitAndLoss(model, isIncome: true);
            var expenses = QueryListProfitAndLoss(model, isIncome: false);


            //cost of good sold
            var costOfGoodsSold = _unitOfWork.TransactionDetailRepository.GetAll(t =>
                           (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                           && (t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                     .Include(x => x.TransactionCategory)
                     .Where(x => x.TransactionCategory.Code.Equals(TransactionCategoryCode.COST_OF_GOOGS_SOLD))
                     .Sum(x => x.Balance);
            // gross profit

            var grossProfit = incomes.Result.Sum(x => x.Balance) - costOfGoodsSold;
            var result = new ProfitAndLossViewWebModel
            {
                Incomes = incomes.Result,
                Expenses = expenses.Result,
                GrossProfit = grossProfit,
                CostOfGoodsSold = costOfGoodsSold
            };
            return result;
        }

        /// <summary>
        /// Get List Profit and loss item
        /// </summary>
        /// <param name="model">The Profit and loss search model</param>
        /// <param name="isIncome">Is Income or Expense</param>
        /// <returns></returns>
        private async Task<List<ProfitAndLossItemModel>> QueryListProfitAndLoss(ProfitAndLossSearchModel model, bool isIncome)
        {

            var result = _unitOfWork.TransactionCategoryRepository.GetAll(x => x.Code != TransactionCategoryCode.COST_OF_GOOGS_SOLD)
                .Include(x => x.TransactionType)
                .Where(x => x.TransactionType.IsDebit == isIncome)
                .Select(x => new
                {
                    ID = x.Id,
                    x.Name,
                    // Caculate Balance base by transaction type
                    TotalBalance = x.TransactionDetails.Where(t =>
                        (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                        && (t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                        .Sum(t => t.Balance)
                })
                .Where(x => x.TotalBalance > 0)
                .Distinct()
                .ToList()
                // Group by Transaction Category ID
                .GroupBy(x => x.ID)
                .Select(g => new ProfitAndLossItemModel
                {
                    Name = g.Select(x => x.Name).FirstOrDefault(),
                    Balance = g.Sum(x => x.TotalBalance)
                })
                .Where(x => x.Balance > 0)
                .ToList();
            return result;
        }



        public async Task<byte[]> Export(ProfitAndLossSearchModel model)
        {
            // get query data
            var data = await QueryProfitAndLossView(model);
            var a = new AccountingPeriod();
            if (model.AccountingPeriodId == null)
            {
                a = _unitOfWork.AccountingPeriodRepository.GetCurrentAccountPeriod();
            }
            else
            {
                a = _unitOfWork.AccountingPeriodRepository.GetById(model.AccountingPeriodId.Value);
            }
            return await ExportExcel(data, a);

        }

        private async Task<byte[]> ExportExcel(ProfitAndLossViewWebModel data, AccountingPeriod a)
        {
            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage(new MemoryStream()))
            {
                // Set some properties
                excelPackage.Workbook.Properties.Title = "P&L";

                // Add worksheet
                var workSheet = excelPackage.Workbook.Worksheets.Add("P&L");

                var currentRow = 1;

                // Add title
                using (ExcelRange Title = workSheet.Cells[currentRow, 1, currentRow, 6])
                {
                    Title.Value = "Profit and loss statement";
                    Title.Merge = true;
                    Title.Style.Font.Size = 15;
                    Title.Style.Font.Bold = true;
                    // Title.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // Title.Style.Fill.BackgroundColor.SetColor(BackGRColor);
                    Title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                currentRow++;

                using (ExcelRange Title = workSheet.Cells[currentRow, 1, currentRow, 6])
                {
                    Title.Value = $"{a.StartDate.Date.ToFormal()} - {a.CloseDate.Date.ToFormal()}";
                    Title.Merge = true;
                    Title.Style.Font.Size = 13;
                    Title.Style.Font.Bold = true;
                    // Title.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // Title.Style.Fill.BackgroundColor.SetColor(BackGRColor);
                    Title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                currentRow++;

                using (ExcelRange Title = workSheet.Cells[currentRow, 1, currentRow, 6])
                {
                    Title.Value = "Export in " + DateTime.Now.ToFormal();
                    Title.Merge = true;
                    Title.Style.Font.Size = 11;
                    Title.Style.Font.Bold = true;
                    // Title.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // Title.Style.Fill.BackgroundColor.SetColor(BackGRColor);
                    Title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                currentRow++;

                // Add title
                // Set style for title
                using (ExcelRange subTitle = workSheet.Cells[currentRow, 1, currentRow, 6])
                {
                    subTitle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    subTitle.Style.Fill.BackgroundColor.SetColor(TitleBGRColor);
                    subTitle.Style.Font.Size = 11;
                    subTitle.Style.Font.Bold = true;
                    subTitle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    subTitle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }


                //set value for Header
                workSheet.Cells[currentRow, 1].Value = "No";
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 3])
                {
                    ranger.Value = "Description";
                    ranger.Merge = true;
                }
                workSheet.Cells[currentRow, 4].Value = "Percent";
                workSheet.Cells[currentRow, 5].Value = "Account";
                workSheet.Cells[currentRow, 6].Value = "Balance";
                currentRow++;

                //Total income

                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.BackgroundColor.SetColor(YellowBGRColor);
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 6])
                {
                    ranger.Merge = true;
                    ranger.Value = "Incomes";
                    ranger.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ranger.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                currentRow++;

                // List incomes
                workSheet.Cells[currentRow, 1, data.Incomes.Count + currentRow, 6].Style.Font.Size = 10;
                var totalIncome = data.Incomes.Sum(x => x.Balance);
                for (int row = 0; row < data.Incomes.Count; row++)
                {
                    workSheet.Cells[currentRow, 1].Value = row + 1;
                    workSheet.Cells[currentRow, 3].Value = data.Incomes[row].Name;
                    workSheet.Cells[currentRow, 4].Value = (data.Incomes[row].Balance / totalIncome).ToPercent();
                    workSheet.Cells[currentRow, 5].Value = data.Incomes[row].Account;
                    workSheet.Cells[currentRow, 6].Value = data.Incomes[row].Balance;
                    currentRow++;
                }

                // Total income
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 3])
                {
                    ranger.Merge = true;
                    ranger.Value = "Total Income";
                    ranger.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ranger.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                workSheet.Cells[currentRow, 4].Value = "100 %";
                workSheet.Cells[currentRow, 6].Value = totalIncome;
                currentRow++;

                //Cost of goods sold
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.BackgroundColor.SetColor(YellowBGRColor);
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 6])
                {
                    ranger.Merge = true;
                    ranger.Value = "";
                    ranger.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ranger.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                currentRow++;

                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 3])
                {
                    ranger.Merge = true;
                    ranger.Value = "Cost of goods sold";
                    ranger.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ranger.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                workSheet.Cells[currentRow, 4].Value = (data.CostOfGoodsSold / totalIncome).ToPercent();
                workSheet.Cells[currentRow, 6].Value = data.CostOfGoodsSold;
                currentRow++;

                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;

                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.BackgroundColor.SetColor(TitleBGRColor);
                workSheet.Cells[currentRow, 2].Value = "Gross Profit ";
                workSheet.Cells[currentRow, 4].Value = (data.GrossProfit / totalIncome).ToPercent();
                workSheet.Cells[currentRow, 6].Value = data.GrossProfit;
                currentRow++;

                //expense
                var totalExpense = data.Expenses.Sum(x => x.Balance);
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;

                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.BackgroundColor.SetColor(YellowBGRColor);
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                workSheet.Cells[currentRow, 2].Value = "Expenses";
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 6])
                {
                    ranger.Merge = true;
                    ranger.Value = "Expenses";
                    ranger.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ranger.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                currentRow++;

                // Add list expense
                for (int row = 0; row < data.Expenses.Count; row++)
                {
                    workSheet.Cells[currentRow, 1].Value = row + 1;
                    workSheet.Cells[currentRow, 3].Value = data.Expenses[row].Name;
                    workSheet.Cells[currentRow, 4].Value = (data.Expenses[row].Balance / totalIncome).ToPercent();
                    workSheet.Cells[currentRow, 5].Value = data.Expenses[row].Account;
                    workSheet.Cells[currentRow, 6].Value = data.Expenses[row].Balance;
                    currentRow++;
                }

                //total expense
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                using (ExcelRange ranger = workSheet.Cells[currentRow, 2, currentRow, 3])
                {
                    ranger.Merge = true;
                    ranger.Value = "Total Expense";
                    ranger.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ranger.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                workSheet.Cells[currentRow, 4].Value = (totalExpense / totalIncome).ToPercent();
                workSheet.Cells[currentRow, 6].Value = totalExpense;
                currentRow++;

                // Net profit
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Font.Bold = true;
                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;

                workSheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.BackgroundColor.SetColor(TitleBGRColor);

                workSheet.Cells[currentRow, 2].Value = "Net Profit: ";
                workSheet.Cells[currentRow, 4].Value = ((data.GrossProfit - totalExpense) / totalIncome).ToPercent();
                workSheet.Cells[currentRow, 6].Value = data.GrossProfit - totalExpense;
                currentRow++;

                // Autofit column
                int minimumSize = 10;
                int maximumSize = 50;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);

                //Add border
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                //Remove border of title
                //workSheet.Cells[2, 2, 2, columns.Count].Style.Border.Top.Style = ExcelBorderStyle.None;

                //Binding excel
                excelPackage.Save();
                return excelPackage.GetAsByteArray();
            }
        }

        public async Task<GenericResult> GetProfitAndLossMobile(ProfitAndLossSearchModel model)
        {
            // get query data
            var result = await GetProfitAndLossMobileView(model);

            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        private async Task<ProfitAndLossViewMobileModel> GetProfitAndLossMobileView(ProfitAndLossSearchModel model)
        {
            AccountingPeriod accountingPeriod;
            if (model.AccountingPeriodId == null)
            {
                accountingPeriod = _unitOfWork.AccountingPeriodRepository.GetCurrentAccountPeriod();
                model.AccountingPeriodId = accountingPeriod.Id;
            }
            else
            {
                accountingPeriod = _unitOfWork.AccountingPeriodRepository.GetById(model.AccountingPeriodId.Value);
            }

            var incomes = QueryListProfitAndLoss(model, true);
            var expense = QueryListProfitAndLoss(model, false);

            //cost of good sold
            var costOfGoodsSold = _unitOfWork.TransactionDetailRepository.GetAll(t =>
                           (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                           && (t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                     .Include(x => x.TransactionCategory)
                     .Where(x => x.TransactionCategory.Code.Equals(TransactionCategoryCode.COST_OF_GOOGS_SOLD))
                     .Sum(x => x.Balance);

            // gross profit
            var grossProfit = incomes.Result.Sum(x => x.Balance) - costOfGoodsSold;

            ProfitAndLossViewMobileModel view = new ProfitAndLossViewMobileModel
            {
                ClosedDate = accountingPeriod.CloseDate,
                StartedDate = accountingPeriod.StartDate,
                Incomes = new GroupProfitAndLossItemModel
                {
                    Title = "Income",
                    EndTitle = "Total Income",
                    TotalAmount = incomes.Result.Sum(x => x.Balance),
                    ListCategory = incomes.Result
                },
                Expenses = new GroupProfitAndLossItemModel
                {
                    Title = "Expense",
                    EndTitle = "Expense",
                    TotalAmount = expense.Result.Sum(x => x.Balance),
                    ListCategory = expense.Result
                },
                CostOfGoodsSold = new GroupProfitAndLossItemModel
                {
                    Title = "Cost of goods sold",
                    EndTitle = "Cost of goods sold",
                    TotalAmount = costOfGoodsSold,
                    ListCategory = null
                },
                GrossProfit = grossProfit,
                NetProfit = grossProfit - costOfGoodsSold
            };

            return view;
        }
    }
}

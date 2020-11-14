﻿using Microsoft.EntityFrameworkCore;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
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
    }

    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

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
            var result = await GetPieByTranactionType(model, false);
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
            var result = _unitOfWork.TransactionCategoryRepository.GetAll(x => x.IsDebit == isDebit)
                .Select(x => new DashboardPieViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalBalance = x.TransactionDetails.Where(t =>
                        (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                        && (model.AccountingPeriodId == null || t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                        .Sum(t => t.Balance)
                }).ToList();

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
            var result = await GetPieByTranactionType(model, true);
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
            // Get current accounting period if accounting period id null
            if (model.AccountingPeriodId == null)
            {
                model.AccountingPeriodId = _unitOfWork.AccountingPeriodRepository
                    .GetAll(x => x.StartDate <= DateTime.Now && x.CloseDate >= DateTime.Now)
                    .FirstOrDefault().Id;
            }

            var incomes = GetProfitAndLoss(model, isIncome: true);
            var expenses = GetProfitAndLoss(model, isIncome: false);
            // Cost of goods sold
            var grossProfit = _unitOfWork.TransactionDetailRepository.GetAll(t =>
                        (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                        && (t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                    .Include(x => x.TransactionCategory)
                    .Where(x => x.TransactionCategory.IsDebit == true)
                    .Sum(x => x.Balance);
            var result = new ProfitAndLossViewModel
            {
                Incomes = incomes.Result,
                Expenses = expenses.Result,
                GrossProfit = grossProfit
            };
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        /// <summary>
        /// Get List Profit and loss item
        /// </summary>
        /// <param name="model">The Profit and loss search model</param>
        /// <param name="isIncome">Is Income or Expense</param>
        /// <returns></returns>
        private async Task<List<ProfitAndLossItemModel>> GetProfitAndLoss(ProfitAndLossSearchModel model, bool isIncome)
        {

            var result = _unitOfWork.TransactionCategoryRepository.GetAll()
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
    }
}

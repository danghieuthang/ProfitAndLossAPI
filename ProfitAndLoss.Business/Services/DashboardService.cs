using Microsoft.EntityFrameworkCore;
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
    }

    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionDetailRepository _transactionDetailRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IAccountingPeriodInStoreRepository _accountingPeriodInStoreRepository;
        private readonly IAccountingPeriodRepository _accountingPeriodRepository;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = unitOfWork.TransactionRepository;
            _transactionDetailRepository = unitOfWork.TransactionDetailRepository;
            _transactionCategoryRepository = unitOfWork.TransactionCategoryRepository;
            _transactionTypeRepository = unitOfWork.TransactionTypeRepository;
            _accountingPeriodInStoreRepository = unitOfWork.AccountingPeriodInStoreRepository;
            _accountingPeriodRepository = unitOfWork.AccountingPeriodRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> GetExpensePie(DashboardSearchModel model)
        {
            var result = await GetPie(model, false);
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        private async Task<List<DashboardPieViewModel>> GetPie(DashboardSearchModel model, bool isDebit)
        {
            //var result =  _transactionCategoryRepository.GetAll(x => x.TransactionType.IsDebit == isDebit).ToList();

            //var result = _transactionCategoryRepository.GetAll()
            //        .Join(_transactionTypeRepository.GetAll(x => x.IsDebit == isDebit), x => x.TransactionTypeId, y => y.Id,
            //        (x, y) =>
            //        new
            //        {
            //            x.Id,
            //            x.Name
            //        })
            //        .Join(_transactionDetailRepository.GetAll(), x => x.Id, y => y.TransactionCategoryId,
            //        (x, y) =>
            //        new
            //        {
            //            x.Id,
            //            x.Name,
            //            y.TotalBalance,
            //            y.AccountingPeriodInStoreId,
            //        })
            //        .Join(_accountingPeriodInStoreRepository.GetAll(
            //            x => (model.StoreId == null || x.StoreId == model.StoreId.Value)
            //              && (model.AccountingPeriodId == null || x.AccountingPeriodId == model.AccountingPeriodId.Value))
            //            , x => x.AccountingPeriodInStoreId, y => y.Id,
            //        (x, y) =>
            //        new
            //        {
            //            x.Id,
            //            x.Name,
            //            x.TotalBalance
            //        })
            //        .GroupBy(x => x.Id)
            //        .Select(group =>
            //        new DashboardPieViewModel
            //        {
            //            TransactionCategoryId = group.Key,
            //            //TransactionCategoryName = group.Select(x=>x.Name).FirstOrDefault(),
            //            TotalBalance = group.Sum(x => x.TotalBalance)
            //        })
            //        .ToList();
            var result = _transactionCategoryRepository.GetAll(x => x.TransactionType.IsDebit == isDebit)
                .Select(x => new DashboardPieViewModel
                {
                    TransactionCategoryId = x.Id,
                    TransactionCategoryName = x.Name,
                    TotalBalance = x.TransactionDetails.Where(t =>
                        (model.StoreId == null || t.AccountingPeriodInStore.StoreId == model.StoreId.Value)
                        && (model.AccountingPeriodId == null || t.AccountingPeriodInStore.AccountingPeriodId == model.AccountingPeriodId.Value))
                        .Sum(t => t.Balance)
                }).ToList();

            return result;
        }
        public async Task<GenericResult> GetRevenuesPie(DashboardSearchModel model)
        {
            var result = await GetPie(model, true);
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
            if (model.AccountingPeriodId == null)
            {
                model.AccountingPeriodId = _accountingPeriodRepository.GetAll(x =>
                        x.StartDate <= DateTime.Now
                        && x.CloseDate >= DateTime.Now
                        ).FirstOrDefault().Id;
            }

            var result = _transactionDetailRepository.GetAll(x =>
            (model.StoreId == null || x.AccountingPeriodInStore.StoreId == model.StoreId.Value)
            && (model.AccountingPeriodId == x.AccountingPeriodInStore.AccountingPeriodId))
                .Include(x=>x.TransactionCategory.TransactionType)
                .ToList()
                .Select(x => new
                {
                    x.Balance,
                    Date = x.CreatedDate.GetMonthYear(),
                    x.TransactionCategory.TransactionType.IsDebit
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
    }
}

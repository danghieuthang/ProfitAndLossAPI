﻿using Microsoft.EntityFrameworkCore;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionServices : IBaseServices<Transaction>
    {
        Task<GenericResult> CreateTransactions(List<TransactionCreateModel> models);
        Task<GenericResult> GetAllByReceiptId(Guid id);
        Task<GenericResult> UpdateTransaction(List<TransactionUpdateModel> models);
    }
    public class TransactionServices : BaseServices<Transaction>, ITransactionServices
    {
        public TransactionServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        private void PrepareCreateEntity()
        {

        }

        public async Task<GenericResult> CreateTransactions(List<TransactionCreateModel> models)
        {
            // get receipt
            var receipt = _unitOfWork.ReceiptRepository.GetAll(x => x.Id == models.FirstOrDefault().TransactionId)
                                                   .Include(x => x.Store).FirstOrDefault();
            if (models.Sum(x => x.Balance) != receipt.SubTotal)
            {
                return new GenericResult
                {
                    Data = null,
                    Message = "Total balance of transaction not equal total balance spited!",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ResultCode = Utilities.AppResultCode.FailValidation,
                    Success = false
                };
            }
            // Get total count of transaction
            int transactionCount = (BaseRepository.GetAll().Count() + 1);
            // Add list tranasction detail
            foreach (var transaction in models)
            {
                var transactionModel = GetTransactionCreate(transaction, transactionCount);
                BaseRepository.Add(transaction.ToEntity());
                transactionCount++;

            }
            // Change status tran to SPLITED
            receipt.Status = (int)ReceiptStatus.SPLITED;
            _unitOfWork.ReceiptRepository.Update(receipt);
            _unitOfWork.Commit();
            return new GenericResult { Success = true, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<GenericResult> GetAllByReceiptId(Guid id)
        {
            var data = BaseRepository.GetAll(x => x.ReceiptId == id).AsNoTracking()
                                    .Include(x => x.TransactionCategory)
                             .Join(_unitOfWork.AccountingPeriodInStoreRepository.GetAll()
                                        .Include(y => y.Store)
                                        .Include(y => y.AccountingPeriod),
                              x => x.AccountingPeriodInStoreId, y => y.Id,
                             (x, y) => new
                             {
                                 x.Id,
                                 x.Description,
                                 x.Balance,
                                 x.TransactionCategory,
                                 Store = new { y.Store.Id, y.Store.Code, y.Store.Name },
                                 AccountingPeriod = new { y.AccountingPeriod.Id, y.AccountingPeriod.Title }
                             })
                                    .ToList();
            //var result = new List<TransactionDetailViewModel>();
            //Global.Mapper.Map(data, result);
            if (data == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.NotFound,
                    Success = true,
                    ResultCode = Utilities.AppResultCode.NotFound,
                    Message = EnumHelper.GetDisplayValue(Utilities.AppResultCode.NotFound)
                };
            }
            return new GenericResult
            {
                Data = data,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Create A Transaction Detail
        /// </summary>
        /// <param name="transaction">The transaction detail create model</param>
        /// <param name="transactionCount">The transaction count</param>
        /// <returns></returns>
        public async Task<Transaction> GetTransactionCreate(TransactionCreateModel transaction, int transactionCount = 1)
        {

            var accountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.GetAll(x => x.StoreId == transaction.StoreId && x.AccountingPeriodId == transaction.AccountingPeriodId).FirstOrDefault();
            //Get accounting Period
            // Create accounting Period In Store if none exists
            if (accountingPeriodInStore == null)
            {
                // Get tranasction period
                var accoungtingPeriod = _unitOfWork.AccountingPeriodRepository.GetById(transaction.AccountingPeriodId);
                // Get receipt
                var receipt = _unitOfWork.ReceiptRepository.GetAll(x => x.Id == transaction.TransactionId)
                    .Include(x => x.Store).FirstOrDefault();

                AccountingPeriodInStore acountingPeriodInStore = new AccountingPeriodInStore
                {
                    Actived = true,
                    AccountingPeriodId = transaction.AccountingPeriodId,
                    StartedDate = accoungtingPeriod.StartDate,
                    ClosedDate = accoungtingPeriod.CloseDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Title = $"{accoungtingPeriod.Title}-{receipt.Store.Code.ToFormal()}",
                    Description = string.Empty,
                    Status = 1,
                    StoreId = transaction.StoreId
                };
                accountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            transaction.AccountingPeriodInStoreId = accountingPeriodInStore.Id;
            transaction.Code = "TD-" + transactionCount.ToString("0000");
            return transaction.ToEntity();
        }

        public async Task<GenericResult> UpdateTransaction(List<TransactionUpdateModel> models)
        {

            //Update list transaction detail
            foreach (var item in models)
            {
                var transaction = _unitOfWork.TransactionRepository.GetById(item.Id);
                var accountinPeriodInStore = await GetAccountingPeriodInStore(item.AccountingPeriodId, item.StoreId, transaction.ReceiptId);
                transaction.AccountingPeriodInStoreId = accountinPeriodInStore.Id;
                transaction.Description = item.Description;
                transaction.Status = TransactionStatus.APPROVAL;
                _unitOfWork.TransactionRepository.Update(transaction);
            }

            _unitOfWork.Commit();
            return new GenericResult
            {
                Success = true,
                ResultCode = Utilities.AppResultCode.Success,
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Get Accounting period in store
        /// </summary>
        /// <param name="accountingPeriodId">The accounting period id</param>
        /// <param name="storeID">The store id</param>
        /// <param name="receiptID">The tranaction ID</param>
        /// <returns></returns>
        private async Task<AccountingPeriodInStore> GetAccountingPeriodInStore(Guid accountingPeriodId, Guid storeID, Guid? receiptID)
        {
            if (receiptID == null)
            {
                return null;
            }

            // Get accounting period in store 
            AccountingPeriodInStore acountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.GetAll(x => x.AccountingPeriodId == accountingPeriodId && x.StoreId == storeID).FirstOrDefault();

            if (acountingPeriodInStore == null)
            {
                var accountingPeriod = _unitOfWork.AccountingPeriodRepository.GetById(accountingPeriodId);

                // Get receipt
                var receipt = _unitOfWork.ReceiptRepository.GetById(receiptID.Value);

                acountingPeriodInStore = new AccountingPeriodInStore
                {
                    Actived = true,
                    AccountingPeriodId = accountingPeriodId,
                    StartedDate = accountingPeriod.StartDate,
                    ClosedDate = accountingPeriod.CloseDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Title = $"{accountingPeriod.Title}-{receipt.Store.Code.ToFormal()}",
                    Description = string.Empty,
                    Status = 1,
                    StoreId = storeID
                };
                acountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            return acountingPeriodInStore;
        }
    }

}

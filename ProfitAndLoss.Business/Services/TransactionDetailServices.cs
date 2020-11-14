using Microsoft.EntityFrameworkCore;
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
    public interface ITransactionDetailServices : IBaseServices<TransactionDetail>
    {
        Task<GenericResult> CreateTransactionDetails(List<TransactionDetailCreateModel> models);
        Task<GenericResult> GetAllByTransactionId(Guid id);
        Task<GenericResult> UpdateTransactionDetails(List<TransactionDetailUpdateModel> models);
    }
    public class TransactionDetailServices : BaseServices<TransactionDetail>, ITransactionDetailServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountingPeriodInStoreRepository _accountingPeriodInStoreRepository;
        private readonly IAccountingPeriodRepository _accountingPeriodRepository;
        public TransactionDetailServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _transactionRepository = unitOfWork.TransactionRepository;
            _accountingPeriodInStoreRepository = unitOfWork.AccountingPeriodInStoreRepository;
            _accountingPeriodRepository = unitOfWork.AccountingPeriodRepository;
        }
        private void PrepareCreateEntity()
        {

        }

        public async Task<GenericResult> CreateTransactionDetails(List<TransactionDetailCreateModel> models)
        {
            var transaction = _transactionRepository.GetAll(x => x.Id == models.FirstOrDefault().TransactionId)
                                                   .Include(x => x.Store).FirstOrDefault();
            if (models.Sum(x => x.Balance) != transaction.SubTotal)
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
            foreach (var transactionDetail in models)
            {
                var transactionDetailModel = GetTransactionDetail(transactionDetail, transactionCount);
                BaseRepository.Add(transactionDetail.ToEntity());
                transactionCount++;

            }
            // Change status tran to SPLITED
            transaction.Status = (int)TransactionStatus.SPLITED;
            _transactionRepository.Update(transaction);
            _unitOfWork.Commit();
            return new GenericResult { Success = true, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<GenericResult> GetAllByTransactionId(Guid id)
        {
            var data = BaseRepository.GetAll(x => x.TransactionId == id).AsNoTracking()
                                    .Include(x => x.TransactionCategory)
                             .Join(_accountingPeriodInStoreRepository.GetAll()
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
        /// <param name="transactionDetail">The transaction detail create model</param>
        /// <param name="transactionCount">The transaction count</param>
        /// <returns></returns>
        public async Task<TransactionDetail> GetTransactionDetail(TransactionDetailCreateModel transactionDetail, int transactionCount = 1)
        {

            var accountingPeriodInStore = _accountingPeriodInStoreRepository.GetAll(x => x.StoreId == transactionDetail.StoreId && x.AccountingPeriodId == transactionDetail.AccountingPeriodId).FirstOrDefault();
            //Get accounting Period
            // Create accounting Period In Store if none exists
            if (accountingPeriodInStore == null)
            {
                // Get tranasction period
                var accoungtingPeriod = _accountingPeriodRepository.GetById(transactionDetail.AccountingPeriodId);
                // Get transaction
                var transaction = _transactionRepository.GetAll(x => x.Id == transactionDetail.TransactionId)
                    .Include(x => x.Store).FirstOrDefault();

                AccountingPeriodInStore acountingPeriodInStore = new AccountingPeriodInStore
                {
                    Actived = true,
                    AccountingPeriodId = transactionDetail.AccountingPeriodId,
                    StartedDate = accoungtingPeriod.StartDate,
                    ClosedDate = accoungtingPeriod.CloseDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Title = $"{accoungtingPeriod.Title}-{transaction.Store.Code.ToFormal()}",
                    Description = string.Empty,
                    Status = 1,
                    StoreId = transactionDetail.StoreId
                };
                accountingPeriodInStore = _accountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            transactionDetail.AccountingPeriodInStoreId = accountingPeriodInStore.Id;
            transactionDetail.Code = "TD-" + transactionCount.ToString("0000");
            return transactionDetail.ToEntity();
        }

        public async Task<GenericResult> UpdateTransactionDetails(List<TransactionDetailUpdateModel> models)
        {

            //Update list transaction detail
            foreach (var item in models)
            {
                var transactionDetail = _unitOfWork.TransactionDetailRepository.GetById(item.Id);
                var accountinPeriodInStore = await GetAccountingPeriodInStore(item.AccountingPeriodId, item.StoreId, transactionDetail.TransactionId);
                transactionDetail.AccountingPeriodInStoreId = accountinPeriodInStore.Id;
                transactionDetail.Description = item.Description;
                _unitOfWork.TransactionDetailRepository.Update(transactionDetail);
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
        /// <param name="transactionID">The tranaction ID</param>
        /// <returns></returns>
        private async Task<AccountingPeriodInStore> GetAccountingPeriodInStore(Guid accountingPeriodId, Guid storeID, Guid? transactionID)
        {
            if (transactionID == null)
            {
                return null;
            }

            // Get accounting period in store 
            AccountingPeriodInStore acountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.GetAll(x => x.AccountingPeriodId == accountingPeriodId && x.StoreId == storeID).FirstOrDefault();

            if (acountingPeriodInStore == null)
            {
                var accountingPeriod = _accountingPeriodRepository.GetById(accountingPeriodId);

                // Get transaction
                var transaction = _transactionRepository.GetById(transactionID.Value);

                acountingPeriodInStore = new AccountingPeriodInStore
                {
                    Actived = true,
                    AccountingPeriodId = accountingPeriodId,
                    StartedDate = accountingPeriod.StartDate,
                    ClosedDate = accountingPeriod.CloseDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Title = $"{accountingPeriod.Title}-{transaction.Store.Code.ToFormal()}",
                    Description = string.Empty,
                    Status = 1,
                    StoreId = storeID
                };
                acountingPeriodInStore = _accountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            return acountingPeriodInStore;
        }
    }

}

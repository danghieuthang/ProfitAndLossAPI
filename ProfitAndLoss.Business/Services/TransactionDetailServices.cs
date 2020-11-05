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
            if (models.Sum(x => x.Balance) != transaction.Balance)
            {
                return new GenericResult
                {
                    Data = null,
                    Message = "Total balance of transaction not equal total balance spited!",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Success = false
                };
            }

            int countTransaction = (BaseRepository.GetAll().Count() + 1);
            foreach (var transactionDetail in models)
            {
                // Get accoungingPeriod in store
                var accountingPeriodInStore = _accountingPeriodInStoreRepository.GetAll(x => x.StoreId == transactionDetail.StoreId && x.AccountingPeriodId == transactionDetail.AccountingPeriodId).FirstOrDefault();
                //Get accounting Period
                var accoungtingPeriod = _accountingPeriodRepository.GetById(transactionDetail.AccountingPeriodId);
                // Create accounting Period In Store if none exists
                if (accountingPeriodInStore == null)
                {
                    AccountingPeriodInStoreCreateModel accountingPeriodCreateModel = new AccountingPeriodInStoreCreateModel
                    {
                        Actived = true,
                        AccountingPeriodId = transactionDetail.AccountingPeriodId,
                        StartDate = accoungtingPeriod.StartDate,
                        CloseDate = accoungtingPeriod.CloseDate,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Title = $"{accoungtingPeriod.Title}-{transaction.Store.Code.ToFormal()}",
                        Description = string.Empty,
                        Status = 1,
                        StoreId = transactionDetail.StoreId
                    };
                    accountingPeriodInStore = _accountingPeriodInStoreRepository.Add(accountingPeriodCreateModel.ToEntity());
                }

                transactionDetail.AccountingPeriodInStoreId = accountingPeriodInStore.Id;
                transactionDetail.Code = "TD-" + (countTransaction++).ToString("0000");
                BaseRepository.Add(transactionDetail.ToEntity());
            }
            // Change status tran to SPLITED
            transaction.Status = (int)TransactionStatus.SPLITED;
            _transactionRepository.Update(transaction);
            _unitOfWork.Commit();
            return new GenericResult { Success = true, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<GenericResult> GetAllByTransactionId(Guid id)
        {
            var data = BaseRepository.GetAll(x => x.TransactionId == id)
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
                                 Store = new { y.Store.Code, y.Store.Name },
                                 AccountingPeriod = y.AccountingPeriod.Title 
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
    }
}

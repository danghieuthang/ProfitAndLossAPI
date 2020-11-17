using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptServices : IBaseServices<Receipt>
    {
        Task<GenericResult> Approval(Guid id);

        Task<GenericResult> Reject(Guid id);
        Task<GenericResult> Create(ReceiptCreateModel model);
        Task<GenericResult> Search(ReceiptSearchModel model);
        List<ValidationModel> ValidateModel(ReceiptCreateModel model);
        Task<GenericResult> GetTransactionsByReceiptId(Guid id);
        Task<GenericResult> GetEvidencesByReceiptId(Guid id);
    }
    public class ReceiptServices : BaseServices<Receipt>, IReceiptServices
    {
        private readonly IReceiptHistoryServices _transactionHistoryServices;
        private readonly IMemberServices _memberServices;
        private readonly IReceiptTypeServices _transactionTypeServices;
        private readonly IStoreServices _storeServices;
        private readonly ISupplierServices _supplierServices;

        public ReceiptServices(IUnitOfWork unitOfWork,
            IReceiptHistoryServices transactionHistoryServices,
            IMemberServices memberServices,
            IReceiptTypeServices transactionTypeServices,
             IStoreServices storeServices,
             ISupplierServices supplierServices) : base(unitOfWork)
        {
            _transactionHistoryServices = transactionHistoryServices;
            _memberServices = memberServices;
            _transactionTypeServices = transactionTypeServices;
            _storeServices = storeServices;
            _supplierServices = supplierServices;
        }
        public List<ValidationModel> ValidateModel(ReceiptCreateModel model)
        {
            var lstValidationResults = new List<ValidationModel>();
            /* validate member id */
            if (!string.IsNullOrEmpty(model.CreateMemberId.ToString()))
            {
                if (!_memberServices.IsExist(model.CreateMemberId.Value))
                {
                    lstValidationResults.Add(new ValidationModel()
                    {
                        Data = StringHelpers.HyphensCase(nameof(model.CreateMemberId)),
                        Message = "Create member is not exist"
                    });
                }
            }
            /* validate store id */
            if (!string.IsNullOrEmpty(model.StoreId.ToString()))
            {
                if (!_storeServices.IsExist(model.StoreId.Value))
                {
                    lstValidationResults.Add(new ValidationModel()
                    {
                        Data = StringHelpers.HyphensCase(nameof(model.StoreId)),
                        Message = "Store is not exist"
                    });
                }
            }
            /* validate transaction type id */
            //if (!string.IsNullOrEmpty(model.ReceiptTypeId.ToString()))
            //{
            //    if (!_transactionTypeServices.IsExist(model.ReceiptTypeId.Value))
            //    {
            //        lstValidationResults.Add(new ValidationModel()
            //        {
            //            Data = StringHelpers.HyphensCase(nameof(model.ReceiptTypeId)),
            //            Message = "Transaction type is not exist"
            //        });
            //    }
            //}
            /* validate supplier id */
            //if (!string.IsNullOrEmpty(model.SupplierId.ToString()))
            //{
            //    if (!_supplierServices.IsExist(model.SupplierId.Value))
            //    {
            //        lstValidationResults.Add(new ValidationModel()
            //        {
            //            Data = StringHelpers.HyphensCase(nameof(model.SupplierId)),
            //            Message = "Supplier is not exist"
            //        });
            //    }
            //}
            return lstValidationResults;
        }
        public async Task<GenericResult> Create(ReceiptCreateModel model)
        {
            /*add new supplier if has new */
            // do later
            var receipt = model.ToEntity();

            // get receipt type
            String receiptCode = string.Empty;
            ReceiptType receiptType;
            if (model.ReceiptTypeId != null && model.Category == null)
            {
                receiptType = _transactionTypeServices.GetRepository().GetById(receipt.ReceiptTypeId.Value);
                receiptCode = receiptType?.Code;
            }
            else
            {
                var transactionCategory = _unitOfWork.TransactionCategoryRepository.GetAll(t => t.Code.Equals(model.Category.Trim())).Include(r => r.ReceiptType).FirstOrDefault();
                receiptType = transactionCategory.ReceiptType;
                receiptCode = receiptType?.Code;
            }
            receipt.Code = receiptCode + "-" + (BaseRepository.GetAll().Count() + 1).ToString("0000");
            receipt.ReceiptTypeId = receiptType?.Id;
            receipt.Supplier = null;
            //Create transaction
            Receipt result = _unitOfWork.ReceiptRepository.Add(receipt);
            switch (receiptType.Code)
            {
                case TransactionTypeCode.SALES:
                    var transactions = await GenerateTransactions(receipt);
                    _unitOfWork.TransactionRepository.AddMulti(transactions);
                    break;
                    //case TransactionTypeCode.EXPENSE:
                    //    break;
                    //case TransactionTypeCode.INVOICE:
                    //    break;
                    //case TransactionTypeCode.REVENUES:
                    //    break;
            }
            /* add new receipt */
            var viewModel = new ReceiptViewModel();

            //Add history 

            _unitOfWork.ReceiptHistoryRepository.Add(
                        new ReceiptHistory
                        {
                            Message = $"Created in {DateTime.Now.Date} ",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ReceiptId = receipt.Id,
                            Actived = true
                        });
            // Save
            _unitOfWork.Commit();
            if (result == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                };
            }

            _ = PushNotificationAsync($"/stores/{result.StoreId}/receipts/{result.Id}");

            viewModel.ToModel(result);
            return new GenericResult
            {
                Data = viewModel,
                Success = true,
                StatusCode = HttpStatusCode.Created,
                ResultCode = Utilities.AppResultCode.Success,
                Message = EnumHelper.GetDisplayValue(Utilities.AppResultCode.Success)
            };
        }

        public async Task<List<Transaction>> GenerateTransactions(Receipt receipt)
        {
            var result = new List<Transaction>();
            receipt.Store = _unitOfWork.StoreRepository.GetById(receipt.StoreId.Value);

            // Get Current Accounting Period
            var currentAccouningtPeriod = _unitOfWork.AccountingPeriodRepository.GetCurrentAccountPeriod();

            //Get accounting Period In Store
            var accountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.GetAll(x => x.StoreId == receipt.StoreId && x.AccountingPeriodId == currentAccouningtPeriod.Id).FirstOrDefault();

            // Create accounting Period In Store if none exists
            if (accountingPeriodInStore == null)
            {

                AccountingPeriodInStore acountingPeriodInStore = new AccountingPeriodInStore
                {
                    Actived = true,
                    AccountingPeriodId = currentAccouningtPeriod.Id,
                    StartedDate = currentAccouningtPeriod.StartDate,
                    ClosedDate = currentAccouningtPeriod.CloseDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Title = $"{currentAccouningtPeriod.Title}-{receipt.Store.Code.ToFormal()}",
                    Description = string.Empty,
                    Status = 1,
                    StoreId = receipt.Store.Id
                };
                accountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            //Create transaction detail for shopping fee
            if (receipt.ShippingFee != 0)
                result.Add(new Transaction
                {
                    AccountingPeriodInStoreId = accountingPeriodInStore.Id,
                    Description = "Shipping Fee",
                    Balance = receipt.ShippingFee,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ReceiptId = receipt.Id,
                    TransactionCategoryId = new Guid("6A0EEC6E-8F35-4DCE-91E9-4CD2FBA41747"),
                    Actived = true,
                    Status = TransactionStatus.PROCESS
                });

            //Create transaction detail for shopping fee
            if (receipt.DiscountValue != 0)
                result.Add(new Transaction
                {
                    AccountingPeriodInStoreId = accountingPeriodInStore.Id,
                    Description = "Discount",
                    Balance = receipt.DiscountValue,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ReceiptId = receipt.Id,
                    TransactionCategoryId = new Guid("8A9773CA-6963-4462-A70E-D50033A2D4B4"),
                    Actived = true,
                    Status = TransactionStatus.PROCESS
                });
            return result;
        }

        public async Task<GenericResult> Approval(Guid id)
        {
            var entity = BaseRepository.GetById(id);
            if (entity == null)
            {
                return new GenericResult
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            entity.Status = (int)ReceiptStatus.APPROVED;

            //
            var result = BaseRepository.Update(entity);

            // Sau sửa lại
            string member = "admin";
            ReceiptHistoryCreateModel transactionHistory = new ReceiptHistoryCreateModel
            {
                Status = 1,
                CreatedDate = DateTime.Now,
                Actived = true,
                ModifiedDate = DateTime.Now,
                ReceiptId = id,
                Message = member.ApprovalTransaction()
            };

            await _transactionHistoryServices.Create(transactionHistory);

            _unitOfWork.Commit();
            if (result == null)
            {
                return new GenericResult
                {
                    Data = result,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> Reject(Guid id)
        {
            var entity = BaseRepository.GetById(id);
            if (entity == null)
            {
                return new GenericResult
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            //
            entity.Status = (int)ReceiptStatus.REJECTED;
            var result = BaseRepository.Update(entity);

            // Sau sửa lại
            string member = "admin";
            ReceiptHistoryCreateModel transactionHistory = new ReceiptHistoryCreateModel
            {
                Status = 1,
                CreatedDate = DateTime.Now,
                Actived = true,
                ModifiedDate = DateTime.Now,
                ReceiptId = result.Id,
                Message = member.RejectTransaction()
            };

            await _transactionHistoryServices.Create(transactionHistory);

            _unitOfWork.Commit();
            if (result == null)
            {
                return new GenericResult
                {
                    Data = result,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> Search(ReceiptSearchModel model)
        {
            //

            var queryz = BaseRepository.GetAll(x =>
                                    (model.StoreId == null || x.StoreId == model.StoreId.Value)
                                    && (model.Code.IsEmpty() || x.Code.Contains(model.Code))
                                    && (model.TransactionTypeId == null || x.ReceiptTypeId == model.TransactionTypeId)
                                    && (model.Status == 0 || x.Status == model.Status)
                                    && (model.FromDate == null || x.CreatedDate >= model.FromDate)
                                    && (model.ToDate == null || x.CreatedDate <= model.ToDate))
                                    .Include(x => x.Store).Include(x => x.Store.Brand)
                                    .Include(x => x.Supplier)
                                    .Include(x => x.ReceiptType)
                                    .Include(x => x.Member).ToList();
            var query = queryz.AsQueryable();
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            //
            var pageResult = new PageResult<Receipt>
            {
                PageIndex = currentPage,
                TotalCount = query.Count(),
                TotalPage = (int)Math.Ceiling(query.Count() * 1.0 / pageSize)
            };

            var strOrder = model.SortBy;
            switch (strOrder)
            {
                case "asc":
                    query = query.OrderBy(x => x.CreatedDate).AsQueryable();
                    break;
                case "desc":
                    query = query.OrderByDescending(x => x.CreatedDate).AsQueryable();
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate).AsQueryable();
                    break;
            }
            var entities = query.Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            List<ReceiptViewModel> listResult = new List<ReceiptViewModel>();
            Global.Mapper.Map(entities, listResult);

            pageResult.Results = listResult;
            //
            if (listResult == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            return new GenericResult
            {
                Data = pageResult,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }


        public override async Task<GenericResult> GetById(Guid id)
        {
            var data = BaseRepository.GetAll(x => x.Id == id)
                                    .Include(x => x.Store)
                                    .Include(x => x.Store.Brand)
                                    .Include(x => x.Supplier)
                                    .Include(x => x.ReceiptType)
                                    .FirstOrDefault();
            var model = new ReceiptViewModel();
            model.ToModel(data);
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
                Data = model,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Get transaction details by transaction id
        /// </summary>
        /// <param name="id">The tranasction id</param>
        /// <returns></returns>
        public async Task<GenericResult> GetTransactionsByReceiptId(Guid id)
        {
            var data = _unitOfWork.TransactionRepository.GetAll(x => x.ReceiptId == id).AsNoTracking()
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

        public async Task PushNotificationAsync(string data)
        {

            IFirebaseConfig ifc = new FirebaseConfig()
            {
                AuthSecret = FirebaseAuthenInfo.Sercet,
                BasePath = "https://swdk13.firebaseio.com/"
            };

            IFirebaseClient client = new FirebaseClient(ifc);
            client.Set("news/link", data);

        }

        /// <summary>
        /// Get all evidences by receipId
        /// </summary>
        /// <param name="id">The receiptID</param>
        /// <returns></returns>
        public async Task<GenericResult> GetEvidencesByReceiptId(Guid id)
        {

            var data = _unitOfWork.EvidenceRepository.GetAll(x => x.ReceiptId == id)
                .Select(x => new EvidenceViewModel
                {
                    Id = x.Id,
                    ImgUrl = x.ImgUrl,
                    Name = x.Name
                })
                .ToList();
            return new GenericResult
            {
                Data = data,
                ResultCode = Utilities.AppResultCode.Success,
                StatusCode = HttpStatusCode.OK,
                Success = true
            };

        }
    }
}

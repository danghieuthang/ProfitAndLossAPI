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
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionServices : IBaseServices<Transaction>
    {
        Task<GenericResult> Approval(Guid id);

        Task<GenericResult> Reject(Guid id);
        Task<GenericResult> Create(TransactionCreateModel model);
        Task<GenericResult> Search(TransactionSearchModel model);
        List<ValidationModel> ValidateModel(TransactionCreateModel model);
    }
    public class TransactionServices : BaseServices<Transaction>, ITransactionServices
    {
        private readonly ITransactionHistoryServices _transactionHistoryServices;
        private readonly IMemberServices _memberServices;
        private readonly ITransactionTypeServices _transactionTypeServices;
        private readonly IStoreServices _storeServices;
        private readonly ISupplierServices _supplierServices;
        private readonly IReceiptServices _receiptServices;
        private readonly IReceiptRepository _receiptRepository;
        private readonly ITransactionDetailServices _transactionDetailServices;

        public TransactionServices(IUnitOfWork unitOfWork,
            ITransactionHistoryServices transactionHistoryServices,
            IMemberServices memberServices,
            ITransactionTypeServices transactionTypeServices,
             IStoreServices storeServices,
             ISupplierServices supplierServices,
             IReceiptServices receiptServices,
             ITransactionDetailServices transactionDetailServices) : base(unitOfWork)
        {
            _transactionHistoryServices = transactionHistoryServices;
            _memberServices = memberServices;
            _transactionTypeServices = transactionTypeServices;
            _storeServices = storeServices;
            _supplierServices = supplierServices;
            _receiptServices = receiptServices;
            _receiptRepository = unitOfWork.ReceptRepository;
            _transactionDetailServices = transactionDetailServices;
        }
        public List<ValidationModel> ValidateModel(TransactionCreateModel model)
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
            if (!string.IsNullOrEmpty(model.TransactionTypeId.ToString()))
            {
                if (!_transactionTypeServices.IsExist(model.TransactionTypeId.Value))
                {
                    lstValidationResults.Add(new ValidationModel()
                    {
                        Data = StringHelpers.HyphensCase(nameof(model.TransactionTypeId)),
                        Message = "Transaction type is not exist"
                    });
                }
            }
            /* validate supplier id */
            if (!string.IsNullOrEmpty(model.SupplierId.ToString()))
            {
                if (!_supplierServices.IsExist(model.SupplierId.Value))
                {
                    lstValidationResults.Add(new ValidationModel()
                    {
                        Data = StringHelpers.HyphensCase(nameof(model.SupplierId)),
                        Message = "Supplier is not exist"
                    });
                }
            }
            return lstValidationResults;
        }
        public async Task<GenericResult> Create(TransactionCreateModel model)
        {
            /*add new supplier if has new */
            // do later
            var transaction = model.ToEntity();
            // get transaction type
            TransactionType transactionType = _transactionTypeServices.GetRepository().GetById(transaction.TransactionTypeId.Value);
            transaction.Supplier = null;
            transaction.Code = transactionType.Code + "-" + (BaseRepository.GetAll().Count() + 1).ToString("0000");

            //Create transaction
            Transaction result = _unitOfWork.TransactionRepository.Add(transaction);

            switch (transactionType.Code)
            {
                case TransactionTypeCode.SALES:
                    var transactionDetails = await GenerateTransactionDetails(transaction);
                    _unitOfWork.TransactionDetailRepository.AddMulti(transactionDetails);
                    break;
                    //case TransactionTypeCode.EXPENSE:
                    //    break;
                    //case TransactionTypeCode.INVOICE:
                    //    break;
                    //case TransactionTypeCode.REVENUES:
                    //    break;
            }
            /* add new receipt */
            var viewModel = new TransactionViewModel();

            if (model.Receipt != null)
            {
                model.Receipt.TransactionId = result.Id;
                viewModel.ReceiptId = _receiptRepository.Add(model.Receipt.ToEntity()).Id;
            }

            //Add history 

            _unitOfWork.TransactionHistoryRepository.Add(
                new TransactionHistory
                {
                    Message = $"Created in {DateTime.Now.Date} ",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    TransactionId = transaction.Id,
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

        public async Task<List<TransactionDetail>> GenerateTransactionDetails(Transaction transaction)
        {
            var result = new List<TransactionDetail>();

            // Get Current Accounting Period
            var currentAccouningtPeriod = _unitOfWork.AccountingPeriodRepository.GetAll(x => x.StartDate <= DateTime.Now && DateTime.Now <= x.CloseDate).FirstOrDefault();

            // Get Store
            var store = _unitOfWork.StoreRepository.GetById(transaction.StoreId.Value);

            //Get accounting Period In Store
            var accountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.GetAll(x => x.StoreId == transaction.StoreId && x.AccountingPeriodId == currentAccouningtPeriod.Id).FirstOrDefault();

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
                    Title = $"{currentAccouningtPeriod.Title}-{store.Code.ToFormal()}",
                    Description = string.Empty,
                    Status = 1,
                    StoreId = store.Id
                };
                accountingPeriodInStore = _unitOfWork.AccountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            //Create transaction detail for shopping fee
            result.Add(new TransactionDetail
            {
                AccountingPeriodInStoreId = accountingPeriodInStore.Id,
                Description = "Shipping Fee",
                Balance = transaction.ShippingFee,
                TransactionId = transaction.Id,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                TransactionCategoryId = new Guid("1B58AB8F-7288-4144-92C3-E9B4BAA4F521"),
                Actived = true
            });

            //Create transaction detail for shopping fee
            result.Add(new TransactionDetail
            {
                AccountingPeriodInStoreId = accountingPeriodInStore.Id,
                Description = "Discount",
                Balance = transaction.DiscountValue,
                TransactionId = transaction.Id,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                TransactionCategoryId = new Guid("18A8D756-19A9-4E7A-AB4B-E3438C43C9E8"),
                Actived = true
            });

            //Create transaction detail for cost of goods sold
            result.Add(new TransactionDetail
            {
                AccountingPeriodInStoreId = accountingPeriodInStore.Id,
                Description = "Cost of goods sold",
                Balance = 0,
                TransactionId = transaction.Id,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                TransactionCategoryId = new Guid("6080D750-69D9-4554-97F5-5F90AED3E407"),
                Actived = true
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
            entity.Status = (int)TransactionStatus.APPROVED;

            //
            var result = BaseRepository.Update(entity);

            // Sau sửa lại
            string member = "admin";
            TransactionHistoryCreateModel transactionHistory = new TransactionHistoryCreateModel
            {
                Status = 1,
                CreatedDate = DateTime.Now,
                Actived = true,
                ModifiedDate = DateTime.Now,
                TransactionId = result.Id,
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
            entity.Status = (int)TransactionStatus.REJECTED;
            var result = BaseRepository.Update(entity);

            // Sau sửa lại
            string member = "admin";
            TransactionHistoryCreateModel transactionHistory = new TransactionHistoryCreateModel
            {
                Status = 1,
                CreatedDate = DateTime.Now,
                Actived = true,
                ModifiedDate = DateTime.Now,
                TransactionId = result.Id,
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

        public async Task<GenericResult> Search(TransactionSearchModel model)
        {
            //

            var queryz = BaseRepository.GetAll(x =>
                                    (model.StoreId == null || x.StoreId == model.StoreId.Value)
                                    && (model.Code.IsEmpty() || x.Code.Contains(model.Code))
                                    && (model.TransactionTypeId == null || x.TransactionTypeId == model.TransactionTypeId)
                                    && (model.Status == 0 || x.Status == model.Status)
                                    && (model.FromDate == null || x.CreatedDate >= model.FromDate)
                                    && (model.ToDate == null || x.CreatedDate <= model.ToDate))
                                    .Include(x => x.Store).Include(x => x.Store.Brand)
                                    .Include(x => x.Supplier)
                                    .Include(x => x.TransactionType)
                                    .Include(x => x.Member).ToList();
            var query = queryz.AsQueryable();
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            //
            var pageResult = new PageResult<Transaction>
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
                    query = query.OrderBy(x => x.CreatedDate).AsQueryable();
                    break;
            }
            var entities = query.Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            List<TransactionViewModel> listResult = new List<TransactionViewModel>();
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
                                    .Include(x => x.TransactionType)
                                    .FirstOrDefault();
            var model = new TransactionViewModel();
            model.ToModel(data);
            var receipt = await _receiptServices.GetEntityByTransactionId(data.Id);
            if (receipt != null)
            {
                model.ReceiptId = receipt.Id;
            }
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


    }
}

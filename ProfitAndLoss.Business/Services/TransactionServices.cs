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

        public TransactionServices(IUnitOfWork unitOfWork,
            ITransactionHistoryServices transactionHistoryServices,
            IMemberServices memberServices,
            ITransactionTypeServices transactionTypeServices,
             IStoreServices storeServices,
             ISupplierServices supplierServices,
             IReceiptServices receiptServices) : base(unitOfWork)
        {
            _transactionHistoryServices = transactionHistoryServices;
            _memberServices = memberServices;
            _transactionTypeServices = transactionTypeServices;
            _storeServices = storeServices;
            _supplierServices = supplierServices;
            this._receiptServices = receiptServices;
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
            var entity = model.ToEntity();
            var result = BaseRepository.Add(entity);
            /* add new receipt */
            if (model.Receipt != null)
            {
                model.Receipt.TransactionId = result.Id;
                await _receiptServices.Create(model.Receipt);
            }
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
            var viewModel = new TransactionViewModel();
            viewModel.ToModel(result);
            return new GenericResult
            {
                Data = model,
                Success = true,
                StatusCode = HttpStatusCode.Created,
                ResultCode = Utilities.AppResultCode.Success,
                Message = EnumHelper.GetDisplayValue(Utilities.AppResultCode.Success)
            };
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
            entity.Status = CommonConstants.TransactionStatus.APPROVAL;


            var result = BaseRepository.Update(entity);
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
            entity.Status = CommonConstants.TransactionStatus.REJECT;
            if (entity == null)
            {
                return new GenericResult
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var result = BaseRepository.Update(entity);
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
            var entities = BaseRepository.GetAll().Include(x => x.Store).Include(x => x.Supplier).Include(x => x.TransactionType).Include(x => x.Member).ToList();
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            //
            var pageResult = new PageResult<Transaction>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count,
                TotalPage = (int)Math.Ceiling(entities.Count * 1.0 / pageSize)
            };

            var strOrder = model.SortBy;
            switch (strOrder)
            {
                case "asc":
                    entities = entities.OrderBy(x => x.CreatedDate).ToList();
                    break;
                case "desc":
                    entities = entities.OrderByDescending(x => x.CreatedDate).ToList();
                    break;
                default:
                    break;
            }
            entities = entities.Skip((currentPage - 1) * pageSize)
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
            if (listResult.Count == 0)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.NotFound,
                    Success = true
                };
            }
            return new GenericResult
            {
                Data = pageResult,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}

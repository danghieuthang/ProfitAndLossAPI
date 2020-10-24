using Microsoft.EntityFrameworkCore;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionServices : IBaseService<Transaction>
    {
        Task<GenericResult> Approval(Guid id);

        Task<GenericResult> Reject(Guid id);

        Task<GenericResult> Search(TransactionSearchModel model);
    }
    public class TransactionServices : BaseService<Transaction>, ITransactionServices
    {
        private readonly ITransactionHistoryServices _transactionHistoryServices;

        public TransactionServices(IUnitOfWork unitOfWork,
            ITransactionHistoryServices transactionHistoryServices) : base(unitOfWork)
        {
            _transactionHistoryServices = transactionHistoryServices;
        }
        private void PrepareCreateEntity(TransactionCreateModel transactionCreateModel)
        {

        }
        public override async Task<GenericResult> Create(BaseCreateModel<Transaction> model)
        {
            var entity = model.ToEntity();
            var result = BaseRepository.Add(entity);
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
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<GenericResult> Approval(Guid id)
        {
            var entity = BaseRepository.GetById(id);
            entity.Status = CommonConstants.TransactionStatus.APPROVAL;
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
            var entities = BaseRepository.GetAll().Include(x => x.Store).Include(x => x.Supplier).Include(x => x.TransactionType).ToList();
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            //
            var pageResult = new PageResult<Transaction>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count
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

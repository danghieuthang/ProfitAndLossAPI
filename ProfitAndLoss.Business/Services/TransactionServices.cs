using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionServices : IBaseService<Transaction>
    {
        Task<GenericResult> Approval(Guid id);

        Task<GenericResult> Reject(Guid id);
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
    }
}

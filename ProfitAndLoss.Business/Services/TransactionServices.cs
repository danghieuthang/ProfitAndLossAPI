using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
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
    }
}

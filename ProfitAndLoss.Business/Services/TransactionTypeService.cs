using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionTypeService : IBaseService<TransactionType>
    {
    }
    public class TransactionTypeService : BaseService<TransactionType>, ITransactionTypeService
    {
        public TransactionTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

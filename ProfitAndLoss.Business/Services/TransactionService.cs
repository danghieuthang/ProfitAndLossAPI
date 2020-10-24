using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionService : IBaseService<Transaction>
    {

    }
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        public TransactionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

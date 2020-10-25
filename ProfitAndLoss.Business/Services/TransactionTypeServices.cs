using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionTypeServices : IBaseServices<TransactionType>
    {
    }
    public class TransactionTypeServices : BaseServices<TransactionType>, ITransactionTypeServices
    {
        public TransactionTypeServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

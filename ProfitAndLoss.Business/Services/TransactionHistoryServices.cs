using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionHistoryServices : IBaseServices<TransactionHistory>
    {

    }
    public class TransactionHistoryServices : BaseServices<TransactionHistory>, ITransactionHistoryServices
    {
        public TransactionHistoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        private void PrepareCreateEntity()
        {

        }

    }
}

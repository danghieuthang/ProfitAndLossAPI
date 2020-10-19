using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionHistoryRepository : IBaseRepository<TransactionHistory, Guid>
    {

    }
    public class TransactionHistoryRepository : BaseRepository<TransactionHistory, Guid>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(DataContext context) : base(context)
        {

        }
    }
}

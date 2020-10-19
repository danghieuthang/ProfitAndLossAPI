using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionRepository : IBaseRepository<Transaction, Guid>
    {

    }
    public class TransactionRepository : BaseRepository<Transaction, Guid>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context)
        {

        }
    }
}

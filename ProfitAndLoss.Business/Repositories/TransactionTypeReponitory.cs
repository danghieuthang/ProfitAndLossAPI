using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionTypeRepository : IBaseRepository<TransactionType, Guid>
    {

    }
    public class TransactionTypeRepository : BaseRepository<TransactionType, Guid>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(DataContext context) : base(context)
        {

        }
    }
}

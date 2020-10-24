using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionCategoryRepository : IBaseRepository<TransactionCategory, Guid>
    {

    }
    public class TransactionCategoryRepository : BaseRepository<TransactionCategory, Guid>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(DataContext context) : base(context)
        {

        }
    }
}

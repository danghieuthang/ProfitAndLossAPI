using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionDetailRepository : IBaseRepository<TransactionDetail, Guid>
    {

    }
    public class TransactionDetailRepository : BaseRepository<TransactionDetail, Guid>, ITransactionDetailRepository
    {
        public TransactionDetailRepository(DataContext context) : base(context)
        {

        }
    }
}

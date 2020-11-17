using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptHistoryRepository : IBaseRepository<ReceiptHistory, Guid>
    {

    }
    public class ReceiptHistoryRepository : BaseRepository<ReceiptHistory, Guid>, IReceiptHistoryRepository
    {
        public ReceiptHistoryRepository(DataContext context) : base(context)
        {

        }
    }
}

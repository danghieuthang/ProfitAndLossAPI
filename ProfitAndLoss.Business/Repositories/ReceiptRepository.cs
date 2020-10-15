using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IReceiptRepository : IBaseRepository<Receipt, Guid>
    {

    }
    public class ReceiptRepository : BaseRepository<Receipt, Guid>, IReceiptRepository
    {
        public ReceiptRepository(DataContext context) : base(context)
        {

        }
    }
}

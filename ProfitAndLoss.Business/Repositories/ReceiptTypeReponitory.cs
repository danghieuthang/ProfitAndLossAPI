using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptTypeRepository : IBaseRepository<ReceiptType, Guid>
    {

    }
    public class ReceiptTypeRepository : BaseRepository<ReceiptType, Guid>, IReceiptTypeRepository
    {
        public ReceiptTypeRepository(DataContext context) : base(context)
        {

        }
    }
}

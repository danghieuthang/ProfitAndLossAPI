using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IAccountingPeriodDetailRepository : IBaseRepository<AccountingPeriodInStore, Guid>
    {

    }
    public class AccountingPeriodDetailRepository : BaseRepository<AccountingPeriodInStore, Guid>, IAccountingPeriodDetailRepository
    {
        public AccountingPeriodDetailRepository(DataContext context) : base(context)
        {

        }
    }
}

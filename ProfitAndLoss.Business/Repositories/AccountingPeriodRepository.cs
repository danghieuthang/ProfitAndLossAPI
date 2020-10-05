using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IAccountingPeriodRepository : IBaseRepository<AccountingPeriod, Guid>
    {

    }
    public class AccountingPeriodRepository : BaseRepository<AccountingPeriod, Guid>, IAccountingPeriodRepository
    {
        public AccountingPeriodRepository(DataContext context) : base(context)
        {

        }
    }
}

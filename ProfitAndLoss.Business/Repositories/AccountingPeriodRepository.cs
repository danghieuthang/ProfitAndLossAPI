using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IAccountingPeriodRepository : IBaseRepository<AccountingPeriod, Guid>
    {
        AccountingPeriod GetCurrentAccountPeriod();
    }
    public class AccountingPeriodRepository : BaseRepository<AccountingPeriod, Guid>, IAccountingPeriodRepository
    {
        public AccountingPeriodRepository(DataContext context) : base(context)
        {

        }

        public AccountingPeriod GetCurrentAccountPeriod()
        {
            return GetAll(x => x.StartDate <= DateTime.Now && x.CloseDate >= DateTime.Now).FirstOrDefault();
        }
    }
}

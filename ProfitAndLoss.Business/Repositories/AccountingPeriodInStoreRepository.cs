using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IAccountingPeriodInStoreRepository : IBaseRepository<AccountingPeriodInStore, Guid>
    {

    }
    public class AccountingPeriodInStoreRepository : BaseRepository<AccountingPeriodInStore, Guid>, IAccountingPeriodInStoreRepository
    {
        public AccountingPeriodInStoreRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}

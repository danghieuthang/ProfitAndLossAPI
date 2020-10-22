using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IAccountingPeriodService : IBaseService<AccountingPeriod>
    {

    }
    public class AccountingPeriodService : BaseService<AccountingPeriod>, IAccountingPeriodService
    {
        public AccountingPeriodService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

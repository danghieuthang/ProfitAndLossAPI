using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IAccountingPeriodService : IBaseServices<AccountingPeriod>
    {

    }
    public class AccountingPeriodService : BaseServices<AccountingPeriod>, IAccountingPeriodService
    {
        public AccountingPeriodService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

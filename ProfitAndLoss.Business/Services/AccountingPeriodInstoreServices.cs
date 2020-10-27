using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{

    public interface IAccountingPeriodInStoreServices : IBaseServices<AccountingPeriodInStore>
    {

    }
    public class AccountingPeriodInstoreServices : BaseServices<AccountingPeriodInStore>, IAccountingPeriodInStoreServices
    {
        public AccountingPeriodInstoreServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

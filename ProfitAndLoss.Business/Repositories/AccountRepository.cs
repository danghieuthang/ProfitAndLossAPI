using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IAccountRepository : IBaseRepository<Account, Guid>
    {

    }
    public class AccountRepository : BaseRepository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(DataContext context) : base(context)
        {

        }
    }
}

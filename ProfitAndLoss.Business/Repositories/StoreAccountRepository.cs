using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IStoreAccountRepository : IBaseRepository<StoreAccount, Guid>
    {

    }
    public class StoreAccountRepository : BaseRepository<StoreAccount, Guid>, IStoreAccountRepository
    {
        public StoreAccountRepository(DataContext context) : base(context)
        {

        }
    }
}

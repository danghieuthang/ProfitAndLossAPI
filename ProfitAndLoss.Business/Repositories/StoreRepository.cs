using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IStoreRepository : IBaseRepository<Store, Guid>
    {

    }
    public class StoreRepository : BaseRepository<Store, Guid>, IStoreRepository
    {
        public StoreRepository(DataContext context) : base(context)
        {

        }
    }
}

using ProfitAndLoss.Data.Models;
using System;

namespace ProfitAndLoss.Business.Services
{
    public interface ISupplierRepository : IBaseRepository<Supplier, Guid>
    {

    }
    public class SupplierRepository : BaseRepository<Supplier, Guid>, ISupplierRepository
    {
        public SupplierRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}

using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface ISupplierService : IBaseService<Supplier>
    {

    }

    public class SupplierService: BaseService<Supplier>, ISupplierService
    {
        public SupplierService(IUnitOfWork unitOfWork): base(unitOfWork)
        {

        }
    }

  
}

using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface ISupplierService : IBaseService<Supplier>
    {
        Task<GenericResult> GetAll();
    }

    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _supplierRepository = unitOfWork.SupplierRepository;
        }

        public async Task<GenericResult> GetAll()
        {
            var data = _supplierRepository.GetAll().Select(x => new SupplierViewModel
            {
                Id = x.Id,
                Name = x.Name
            });
            return new GenericResult { Data = data, Success = true, StatusCode = System.Net.HttpStatusCode.OK };
        }
    }


}

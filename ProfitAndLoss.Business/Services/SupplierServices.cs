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
    public interface ISupplierServices : IBaseServices<Supplier>
    {
        Task<GenericResult> GetAll();
    }

    public class SupplierServices : BaseServices<Supplier>, ISupplierServices
    {
        public SupplierServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<GenericResult> GetAll()
        {
            var data = _unitOfWork.SupplierRepository.GetAll().Select(x => new SupplierViewModel
            {
                Id = x.Id,
                Name = x.Name
            });
            return new GenericResult { Data = data, Success = true, StatusCode = System.Net.HttpStatusCode.OK };
        }
    }


}

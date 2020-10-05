using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IBrandService
    {
        Task<GenericResult> CreateBrand(BrandCreateModel model);
    }
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GenericResult> CreateBrand(BrandCreateModel model)
        {
            Brand brand = new Brand { Actived = model.Actived, CreatedDate = System.DateTime.Now };
            _unitOfWork.BrandRepository.Add(brand);
            _unitOfWork.CommitAsync();
            return new GenericResult { Success = true, Message="Create Brand success!" };
        }
    }
}

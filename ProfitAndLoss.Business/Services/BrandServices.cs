﻿using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IBrandServices : IBaseServices<Brand>
    {
        Task<GenericResult> CreateBrand(BrandCreateModel model);
        Task<GenericResult> DeleteAsync(Guid id);
        Task<GenericResult> GetAllBrandAsync();
        Task<GenericResult> GetBrand(Guid id);
    }
    public class BrandServices : BaseServices<Brand>, IBrandServices
    {
        public BrandServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<GenericResult> CreateBrand(BrandCreateModel model)
        {
            Brand brand = new Brand { Actived = model.Actived, CreatedDate = System.DateTime.Now };
            _unitOfWork.BrandRepository.Add(brand);
            _unitOfWork.CommitAsync();
            return new GenericResult { Success = true, Message = "Create Brand success!" };
        }

        public Task<GenericResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> GetAllBrandAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResult> GetBrand(Guid id)
        {
            var brand = _unitOfWork.BrandRepository.GetById(id);
            return new GenericResult
            {
                Data = brand
            };
        }
    }
}

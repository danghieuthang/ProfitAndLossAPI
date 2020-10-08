﻿using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IBrandService : IBaseService<Brand>
    {
        Task<GenericResult> CreateBrand(RequestCreateBrandModel model);
        Task<GenericResult> DeleteAsync(Guid id);
        Task<GenericResult> GetAllBrandAsync();
    }
    public class BrandService : BaseService<Brand>, IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GenericResult> CreateBrand(RequestCreateBrandModel model)
        {
            Brand brand = new Brand { Actived = model.Actived, CreatedDate = System.DateTime.Now };
            _unitOfWork.BrandRepository.Add(brand);
            _unitOfWork.CommitAsync();
            return new GenericResult { Success = true, Message="Create Brand success!" };
        }

        public Task<GenericResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> GetAllBrandAsync()
        {
            throw new NotImplementedException();
        }
    }
}

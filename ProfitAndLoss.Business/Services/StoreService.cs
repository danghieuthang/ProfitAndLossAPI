using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IStoreService : IDisposable
    {
        Task<GenericResult> CreateStore(RequestCreateStoreModel model);
        Task<GenericResult> UpdateStore(RequestUpdateStoreModel model);
        Task<GenericResult> DeleteStore(Guid id);
        Task<GenericResult> GellAllStoreAsync();
        Task<GenericResult> SearchStoreAsync(RequestSearchStoreModel model);
    }
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _storeRepository = _unitOfWork.StoreRepository;
        }
        public async Task<GenericResult> CreateStore(RequestCreateStoreModel model)
        {
            var entity = model.ToEntity();
            _storeRepository.Add(entity);
            return new GenericResult { Data = entity, Success = true };
        }

        public async Task<GenericResult> DeleteStore(Guid id)
        {
            var entity = _storeRepository.GetById(id);
            _storeRepository.Delete(entity);
            return new GenericResult { Data = entity, Success = true };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> GellAllStoreAsync()
        {
            var entities = _storeRepository.GetAll();
            return new GenericResult { Data = entities, Success = true };
        }

        public async Task<GenericResult> SearchStoreAsync(RequestSearchStoreModel model)
        {
            //
            var entities = _storeRepository.GetAll();
            //
            var pageSize = model.PageSize;
            var currentPage = model.Page;
            var strOrder = model.SortBy;
            //
            var result = new PageResult<Store>
            {
                PageSize = pageSize,
                CurrentPage = currentPage,
                RowCount = entities.Count()
            };

            result.Results = entities.Skip((currentPage - 1) * pageSize)
                                    .Take(result.LastRow)
                                    .OrderBy(x => x.CreatedDate)
                                    .ToList();

            //
            return new GenericResult { Data = result, Success = true };
        }

        public async Task<GenericResult> UpdateStore(RequestUpdateStoreModel model)
        {
            var entity = model.ToEntity();
            _storeRepository.Update(entity);
            return new GenericResult { Data = entity, Success = true };
        }
    }
}

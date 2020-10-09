using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IStoreService : IBaseService<Store>
    {
        Task<GenericResult> SearchStoreAsync(RequestSearchStoreModel model);
    }
    public class StoreService : BaseService<Store>, IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public StoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _storeRepository = _unitOfWork.StoreRepository;
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
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            var strOrder = model.SortBy;
            //
            var result = new PageResult<Store>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count()
            };

            result.Results = entities.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            //
            return new GenericResult
            {
                Data = result,
                Success = true
            };
        }

        public Task<GenericResult> Update(BaseUpdateModel<Store> model)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResult> UpdateStore(RequestUpdateStoreModel model)
        {
            var entity = model.ToEntity();
            _storeRepository.Update(entity);
            return new GenericResult { Data = entity, Success = true };
        }

    }
}

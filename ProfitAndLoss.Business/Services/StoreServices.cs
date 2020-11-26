using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
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
    public interface IStoreServices : IBaseServices<Store>
    {
        Task<GenericResult> SearchStoreAsync(StoreSearchModel model);
    }
    public class StoreServices : BaseServices<Store>, IStoreServices
    {
        public StoreServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<GenericResult> GellAllStoreAsync()
        {
            var entities = _unitOfWork.StoreRepository.GetAll();
            return new GenericResult { Data = entities, Success = true };
        }

        public async Task<GenericResult> SearchStoreAsync(StoreSearchModel model)
        {
            //
            var entities = _unitOfWork.StoreRepository.GetAll();
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

        public async Task<GenericResult> UpdateStore(StoreUpdateModel model)
        {
            var entity = model.ToEntity();
            _unitOfWork.StoreRepository.Update(entity);
            return new GenericResult { Data = entity, Success = true };
        }

    }
}

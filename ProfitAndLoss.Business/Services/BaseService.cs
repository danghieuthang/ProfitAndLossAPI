using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IBaseService
    {
    }

    public interface IBaseService<T> : IDisposable, IBaseService
    {
        Task<GenericResult> Create(BaseCreateModel<T> model);
        Task<GenericResult> Update(BaseUpdateModel<T> model);
        Task<GenericResult> Delete(Guid id);
        //     Task<GenericResult> GetById(Guid id);
        Task<GenericResult> GetById(Guid id);
        Task<GenericResult> Search(BaseSearchModel<T> model);

        Task<GenericResult> GetAll();


    }
    public class BaseService<T> : IBaseService<T> where T : BaseEntity<Guid>
    {
        #region fields
        protected readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<T, Guid> _baseRepository;

        #endregion fiedls

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IBaseRepository<T, Guid> BaseRepository
        {
            get
            {
                return _baseRepository ??=
                        (IBaseRepository<T, Guid>)_unitOfWork.GetType()
                                    .GetProperties() // Get All properties in UnitOfWork
                                    .Select(c => c.GetValue(_unitOfWork)) // Select the repositories in UnitOfWork
                                    .FirstOrDefault(x => x is IBaseRepository<T, Guid>);
            }
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<GenericResult> Create(BaseCreateModel<T> model)
        {
            var entity = model.ToEntity();
            var result = BaseRepository.Add(entity);
            _unitOfWork.Commit();

            if (result == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<GenericResult> Delete(Guid id)
        {
            var entity = BaseRepository.GetById(id);
            if (entity == null)
            {
                return new GenericResult
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var result = BaseRepository.Delete(id);
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = result,
                Success = result != null,
                StatusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> GetAll()
        {
            var result = BaseRepository.GetAll();
            if (result == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> GetById(Guid id)
        {
            var result = BaseRepository.GetById(id);
            if (result == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> Search(BaseSearchModel<T> model)
        {
            //
            var entities = BaseRepository.GetAll();
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            var strOrder = model.SortBy;
            //
            var result = new PageResult<T>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count()
            };

            result.Results = entities.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            //
            if (result == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            if (result.Results.Count == 0)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.NotFound,
                    Success = true
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> Update(BaseUpdateModel<T> model)
        {
            var entity = model.ToEntity();
            if (BaseRepository.GetById(entity.Id) == null)
            {
                return new GenericResult
                {
                    Data = entity,
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            var result = BaseRepository.Update(entity);
            _unitOfWork.Commit();
            if (result == null)
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}

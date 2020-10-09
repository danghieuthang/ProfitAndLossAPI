﻿using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
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

    }
    public class BaseService<T> : IBaseService<T> where T : BaseEntity<Guid>
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
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
        public async Task<GenericResult> Create(BaseCreateModel<T> model)
        {
            var entity = model.ToEntity();

            var result = BaseRepository.Add(entity);
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = result,
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<GenericResult> Delete(Guid id)
        {
            var entity = BaseRepository.GetById(id);
            var result = BaseRepository.Delete(entity);
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = result
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> Update(BaseUpdateModel<T> model)
        {
            var entity = model.ToEntity();
            var result = BaseRepository.Update(entity);
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = result
            };
        }
    }
}

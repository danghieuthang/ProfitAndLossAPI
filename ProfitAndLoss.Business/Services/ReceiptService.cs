﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptService : IBaseService<Receipt>
    {
        Task<Receipt> CreateRecept(ReceiptCreateModel model);
        Task<GenericResult> SearchRecepts(ReceiptSearchModel model);
    }
    public class ReceiptService : BaseService<Receipt>, IReceiptService
    {
        private readonly IReceiptTypeRepository _receiptTypeRepository;
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _receiptTypeRepository = unitOfWork.ReceiptTypeRepository;
            _receiptRepository = unitOfWork.ReceptRepository;
        }

        /// <summary>
        /// Create recept
        /// </summary>
        /// <param name="model">The request create recept model</param>
        /// <returns>Return recept created</returns>
        public async Task<Receipt> CreateRecept(ReceiptCreateModel model)
        {
            var entity = model.ToEntity();
            var result = BaseRepository.Add(entity);
            _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<GenericResult> SearchRecepts(ReceiptSearchModel model)
        {
            //
            var entities = _receiptRepository.GetAll()
                            .Join(_receiptTypeRepository.GetAll(), x => x.TypeId, y => y.Id,
                            (x, y) =>
                new ReceiptViewModel
                {
                    Description = x.Description,
                    Type = y.Name,
                    ModifiedDate = x.ModifiedDate,
                    CreatedDate = x.CreatedDate,
                    Status = x.Status
                });
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            var strOrder = model.SortBy;
            //
            var result = new PageResult<Receipt>
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
    }
}

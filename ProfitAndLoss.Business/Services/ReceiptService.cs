using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptService : IBaseService<Receipt>
    {
        Task<GenericResult> SearchRecepts(ReceiptSearchModel model);
    }
    public class ReceiptService : BaseService<Receipt>, IReceiptService
    {
        private readonly IReceiptTypeRepository _receiptTypeRepository;
        private readonly IReceiptRepository _receiptRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IStoreRepository _storeRepository;
        public ReceiptService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _receiptTypeRepository = unitOfWork.ReceiptTypeRepository;
            _receiptRepository = unitOfWork.ReceptRepository;
            _supplierRepository = unitOfWork.SupplierRepository;
            _storeRepository = unitOfWork.StoreRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">The receipt Search model</param>
        /// <returns></returns>
        public async Task<GenericResult> SearchRecepts(ReceiptSearchModel model)
        {
            //
            //var entities = _receiptRepository.GetAll()
            //                .Join(_receiptTypeRepository.GetAll(), x => x.TypeId, y => y.Id,
            //                (x, y) =>
            //                new
            //                {
            //                    x.Id,
            //                    x.SupplierId,
            //                    x.Description,
            //                    Type = y.Name,
            //                    x.StoreId,
            //                    x.ModifiedDate,
            //                    x.CreatedDate,
            //                    x.Status,
            //                    x.Price
            //                })
            //                .Join(_supplierRepository.GetAll(), x => x.SupplierId, y => y.Id,
            //                (x, y) =>
            //                new
            //                {
            //                    x.Id,
            //                    x.StoreId,
            //                    x.Description,
            //                    x.CreatedDate,
            //                    x.ModifiedDate,
            //                    x.Status,
            //                    Supplier = y.Name,
            //                    x.Type,
            //                    x.Price
            //                })
            //                .Join(_storeRepository.GetAll(), x => x.StoreId, y => y.Id,
            //                (x, y) => new ReceiptViewModel
            //                {
            //                    Id = x.Id,
            //                    CreatedDate = x.CreatedDate,
            //                    Description = x.Description,
            //                    Price = x.Price,
            //                    ModifiedDate = x.ModifiedDate,
            //                    Status = x.Status,
            //                    Store = y.Code + "-" + y.Name,
            //                    Supplier = x.Supplier,
            //                    Type = x.Type
            //                });
            //// 
            var entities = _receiptRepository.Entity();
            entities = entities.Include(c => c.ReceiptType).Include(c => c.Supplier).Include( c => c.Store).Include( c => c.Member);
            /* search by name*/

            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            /* sort */
            var strOrder = model.SortBy;
            switch (strOrder)
            {
                case "asc":
                    entities = entities.OrderBy(x => x.CreatedDate);
                    break;
                case "desc":
                    entities = entities.OrderByDescending(x => x.CreatedDate);
                    break;
                default:
                    break;
            }
            /* get by page */
            entities = entities.Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize);

            // 
            List<ReceiptViewModel> listResult = new List<ReceiptViewModel>();
            var listEntities = entities.ToList();
            Global.Mapper.Map(listEntities, listResult);
            var result = new PageResult<Receipt>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count(),
            };
            result.TotalPage = (int)Math.Ceiling(result.TotalCount * 1.0 / pageSize);
            result.Results = listResult;

            //
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}

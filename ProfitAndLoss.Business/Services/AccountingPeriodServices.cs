using Microsoft.EntityFrameworkCore;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IAccountingPeriodServices : IBaseServices<AccountingPeriod>
    {
        Task<GenericResult> Search(AccountingPeriodSearchModel model);
        Task<GenericResult> Create(AccountingPeriodCreateModel model);
        Task<GenericResult> GetAccountingPeriodStillOpen();
        Task<GenericResult> Close(Guid id);
    }
    public class AccountingPeriodServices : BaseServices<AccountingPeriod>, IAccountingPeriodServices
    {
        public AccountingPeriodServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<GenericResult> Search(AccountingPeriodSearchModel model)
        {
            //
            var entities = BaseRepository.GetAll().ToList();
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            //
            var pageResult = new PageResult<Receipt>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count,
                TotalPage = (int)Math.Ceiling(entities.Count * 1.0 / pageSize)
            };

            entities = entities.Where(x => (model.FromDate == null || x.StartDate >= model.FromDate)
                                        && (model.ToDate == null || x.CloseDate <= model.ToDate))
                                .ToList();


            entities = entities.OrderBy(x => x.StartDate).Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            List<AccountingPeriodViewModel> listResult = new List<AccountingPeriodViewModel>();
            Global.Mapper.Map(entities, listResult);

            pageResult.Results = listResult;
            //
            if (listResult == null)
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
                Data = pageResult,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> Create(AccountingPeriodCreateModel model)
        {
            if (model.CloseDate <= model.StartDate)
            {
                return new GenericResult
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Start date must be smaller than close date!"
                };
            }
            var isExists = BaseRepository.GetAll(x =>
                       (x.StartDate <= model.StartDate && x.CloseDate >= model.StartDate)
                     || (x.StartDate <= model.CloseDate && x.CloseDate >= model.CloseDate)
                     || (x.StartDate >= model.StartDate && x.StartDate <= model.CloseDate)
                     || (x.CloseDate >= model.StartDate && x.CloseDate <= model.CloseDate)
                    ).Count();
            if (isExists > 0)
            {
                return new GenericResult
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ResultCode = Utilities.AppResultCode.FailValidation,
                    Message = "Start date or close date was exists!"
                };
            }

            var entity = model.ToEntity();
            entity.Status = AccountingPeriodStatus.OPEN;
            entity = BaseRepository.Add(entity);

            //
            var stores = _unitOfWork.StoreRepository.GetAll().Select(x => x.Id).ToList();
            foreach (var storeID in stores)
            {
                AccountingPeriodInStore acountingPeriodInStore = new AccountingPeriodInStore
                {
                    Actived = true,
                    AccountingPeriodId = entity.Id,
                    StartedDate = entity.StartDate,
                    ClosedDate = entity.CloseDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = string.Empty,
                    Status = 1,
                    StoreId = storeID
                };
                _unitOfWork.AccountingPeriodInStoreRepository.Add(acountingPeriodInStore);
            }

            _unitOfWork.Commit();
            var data = new AccountingPeriodViewModel();
            Global.Mapper.Map(entity, data);
            return new GenericResult
            {
                Data = data,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                ResultCode = Utilities.AppResultCode.Success
            };

        }

        public async Task<GenericResult> GetAccountingPeriodStillOpen()
        {
            var result = _unitOfWork.AccountingPeriodRepository.GetAll(x => x.Status == AccountingPeriodStatus.OPEN).ToList();
            var dataView = new List<AccountingPeriodViewModel>();
            Global.Mapper.Map(result, dataView);
            return new GenericResult
            {
                Data = dataView,
                Success = true,
                ResultCode = Utilities.AppResultCode.Success,
                StatusCode = HttpStatusCode.OK
            };
        }

        //public override async Task<GenericResult> GetById(Guid id)
        //{
        //    var entity = BaseRepository.GetById(id);
        //    var result = new AccountingPeriodViewModel();
        //    Global.Mapper.Map(entity, result);

        //    if (result == null)
        //    {
        //        return new GenericResult
        //        {
        //            Data = null,
        //            StatusCode = HttpStatusCode.NotFound,
        //            Success = true,
        //            ResultCode = Utilities.AppResultCode.NotFound,
        //            Message = EnumHelper.GetDisplayValue(Utilities.AppResultCode.NotFound)
        //        };
        //    }
        //    return new GenericResult
        //    {
        //        Data = result,
        //        Success = true,
        //        StatusCode = HttpStatusCode.OK
        //    };
        //}

        public override async Task<GenericResult> Delete(Guid id)
        {
            var entity = _unitOfWork.AccountingPeriodRepository.GetById(id);
            if (entity == null)
            {
                return new GenericResult
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            entity.Status = AccountingPeriodStatus.CANCEL;
            var result = _unitOfWork.AccountingPeriodRepository.Update(entity);

            _unitOfWork.Commit();

            if (result == null)
            {
                return new GenericResult
                {
                    Data = result,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return new GenericResult
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GenericResult> Close(Guid id)
        {
            var accountingPeriod = _unitOfWork.AccountingPeriodRepository.GetById(id);
            if (accountingPeriod == null)
            {
                return new GenericResult
                {
                    ResultCode = Utilities.AppResultCode.NotFound,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Accounting period not exists",
                    Success = false
                };
            }

            if (accountingPeriod.CloseDate <= DateTime.Now)
            {
                return new GenericResult
                {
                    ResultCode = Utilities.AppResultCode.FailValidation,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Accounting period can't not close in operation time",
                    Success = false
                };
            }

            accountingPeriod.Status = AccountingPeriodStatus.CLOSED;
            return new GenericResult
            {
                Data = accountingPeriod,
                Success = true,
                ResultCode = Utilities.AppResultCode.Success,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}

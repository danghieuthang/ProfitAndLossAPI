using Microsoft.EntityFrameworkCore;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
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
            var pageResult = new PageResult<Transaction>
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
                    Message = "Start date or close date was exists!"
                };
            }

            var entity = model.ToEntity();
            entity = BaseRepository.Add(entity);
            _unitOfWork.Commit();
            var data = new AccountingPeriodViewModel();
            Global.Mapper.Map(entity, data);
            return new GenericResult
            {
                Data = data,
                Success = true,
                StatusCode = HttpStatusCode.OK,
            };

        }
    }
}

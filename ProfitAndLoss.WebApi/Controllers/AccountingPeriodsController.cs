using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.AccountingPeriod.PREFIX)]
    [ApiController]
    public class AccountingPeriodsController : BaseController
    {
        private readonly IAccountingPeriodServices _accountingPeriodService;
        public AccountingPeriodsController(IAccountingPeriodServices accountingPeriodService, IdentityServices identityServices) : base(identityServices)
        {
            _accountingPeriodService = accountingPeriodService;
        }
        /// <summary>
        /// Get all accounting period 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> GetAllAccountingPeriods()
        {
            return await _accountingPeriodService.GetAll();
        }

        [HttpGet("search")]
        public async Task<GenericResult> Search([FromQuery] AccountingPeriodSearchModel model)
        {
            return await _accountingPeriodService.Search(model);
        }
        /// <summary>
        /// Get the accounting periods that are still open
        /// </summary>
        /// <returns></returns>
        [HttpGet("still-open")]
        public async Task<GenericResult> GetAccountingPeriodStillOpen()
        {
            return await _accountingPeriodService.GetAccountingPeriodStillOpen();
        }
        /// <summary>
        /// Get a accounting period by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GenericResult> GetAccountingPeriodById(Guid id)
        {
            return await _accountingPeriodService.GetById(id);
        }

        /// <summary>
        /// Create new accounting period
        /// </summary>
        /// <param name="model">The accounting period create model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> CreateAccountingPeriod([FromBody] AccountingPeriodCreateModel model)
        {
            return await _accountingPeriodService.Create(model);
        }

        /// <summary>
        /// Update accounting period
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<GenericResult> UpdateAccountingPeriod([FromBody] AccountingPeriodUpdateModel model)
        {
            return await _accountingPeriodService.Update(model);
        }

        /// <summary>
        /// Close accounting period
        /// </summary>
        /// <param name="id">The accounting period id</param>
        /// <returns></returns>
        [HttpPut("close/{id}")]
        public async Task<GenericResult> CloseAccountingPeiord(Guid id)
        {
            return await _accountingPeriodService.Close(id);
        }

        /// <summary>
        /// Delete accounting period
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<GenericResult> Delete(Guid id)
        {
            return await _accountingPeriodService.Delete(id);
        }
    }
}

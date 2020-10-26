using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.AccountingPeriod.PREFIX)]
    [ApiController]
    public class AccountingPeriodsController : BaseController
    {
        private readonly IAccountingPeriodService _accountingPeriodService;
        public AccountingPeriodsController(IAccountingPeriodService accountingPeriodService)
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
        public async Task<GenericResult> CreateAccountingPeriod(AccountingPeriodCreateModel model)
        {
            return await _accountingPeriodService.Create(model);
        }

        /// <summary>
        /// Update accounting period
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<GenericResult> UpdateAccountingPeriod(AccountingPeriodUpdateModel model)
        {
            return await _accountingPeriodService.Update(model);
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

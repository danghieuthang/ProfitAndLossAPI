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
    [Route(RouteConstants.Dashboard.PREFIX)]
    [ApiController]
    public class DashboardsController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        public DashboardsController(IDashboardService dashboardService, IdentityServices identityServices) : base(identityServices)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get pie data expense
        /// </summary>
        /// <param name="model">The Dashboard search model</param>
        /// <returns></returns>
        [HttpGet("pie/expense")]
        public async Task<GenericResult> GetExpensePie([FromQuery] DashboardSearchModel model)
        {
            return await _dashboardService.GetExpensePie(model);
        }
        /// <summary>
        /// Get pie data revenues
        /// </summary>
        /// <param name="model">The Dashboard search model</param>
        /// <returns></returns>
        [HttpGet("pie/revenues")]
        public async Task<GenericResult> GetRevenuesPie([FromQuery] DashboardSearchModel model)
        {
            return await _dashboardService.GetRevenuesPie(model);
        }

        [HttpGet("revenue-expenses")]
        public async Task<GenericResult> GetRevenueExpense([FromQuery] DashboardSearchModel model)
        {
            return await _dashboardService.GetRevenueExpense(model);
        }


    }
}

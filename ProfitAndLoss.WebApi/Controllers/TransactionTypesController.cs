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
    [Route(RouteConstants.TransactionType.PREFIX)]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService;
        public TransactionTypesController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        /// <summary>
        /// Create new Transaction Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> Create([FromBody] TransactionTypeCreateModel model)
        {
            return await _transactionTypeService.Create(model);
        }

        /// <summary>
        /// Get all Transaction Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> GetAll()
        {
            return await _transactionTypeService.GetAll();
        }
    }
}

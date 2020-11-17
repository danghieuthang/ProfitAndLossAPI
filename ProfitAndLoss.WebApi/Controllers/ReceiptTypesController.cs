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
    [Route(RouteConstants.ReceiptType.PREFIX)]
    [ApiController]
    public class ReceiptTypesController : BaseController
    {
        private readonly IReceiptTypeServices _receiptTypeService;
        public ReceiptTypesController(IReceiptTypeServices receiptTypeService, IdentityServices identityServices) : base(identityServices)
        {
            _receiptTypeService = receiptTypeService;
        }

        /// <summary>
        /// Create new Transaction Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> Create([FromBody] ReceiptTypeCreateModel model)
        {
            return await _receiptTypeService.Create(model);
        }

        /// <summary>
        /// Get all Transaction Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> GetAll()
        {
            return await _receiptTypeService.GetAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.ReceiptType.PREFIX)]
    [ApiController]
    public class ReceiptTypeController : ControllerBase
    {
        private readonly IReceiptTypeService _receiptTypeService;

        public ReceiptTypeController(IReceiptTypeService receiptTypeService)
        {
            _receiptTypeService = receiptTypeService;
        }

        [HttpPost]
        public async Task<GenericResult> CreateReceiptType([FromBody] ReceiptTypeCreateModel model)
        {
            return await _receiptTypeService.Create(model);
        }
    }
}

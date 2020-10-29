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
    [Route(RouteConstants.TransactionDetail.PREFIX)]
    [ApiController]
    public class TransactionDetailsController : ControllerBase
    {
        private readonly ITransactionDetailServices _transactionDetailServices;
        public TransactionDetailsController(ITransactionDetailServices transactionDetailServices)
        {
            _transactionDetailServices = transactionDetailServices;
        }

        /// <summary>
        /// Split transaction to multi transaction detail
        /// </summary>
        /// <param name="models">List transction detail</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> Creates(List<TransactionDetailCreateModel> models)
        {
            return await _transactionDetailServices.CreateTransactionDetails(models);
        }
    }
}

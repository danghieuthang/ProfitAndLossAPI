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
    [Route(RouteConstants.Transaction.PREFIX)]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionsController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        /// <summary>
        /// Split transaction to multi transaction detail
        /// </summary>
        /// <param name="models">List transction detail</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> Creates(List<TransactionCreateModel> models)
        {

            return await _transactionServices.CreateTransactions(models);
        }

        [HttpPut]
        public async Task<GenericResult> Update(List<TransactionUpdateModel> models)
        {
            return await _transactionServices.UpdateTransaction(models);
        }

        /// <summary>
        /// Get all transaction detail by transaction id
        /// </summary>
        /// <param name="id">The transaction id</param>
        /// <returns></returns>
        [HttpGet("receipt/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<GenericResult> GetTransactionDetailByTransactionId(Guid id)
        {
            return await _transactionServices.GetAllByReceiptId(id);
        }
    }
}

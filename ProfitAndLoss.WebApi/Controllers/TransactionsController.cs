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
        private readonly ITransactionServices _transactionService;
        public TransactionsController(ITransactionServices transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<GenericResult> GetAll()
        {
            return await _transactionService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<GenericResult> GetById(Guid id)
        {
            return await _transactionService.GetById(id);
        }

        /// <summary>
        /// Search transactions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("{params}")]
        public async Task<GenericResult> Search(TransactionSearchModel model)
        {
            return await _transactionService.Search(model);
        }

        [HttpPost]
        public async Task<GenericResult> Create([FromBody]TransactionCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return new GenericResult(){
                    Message = "Invalid object",
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            
            return await _transactionService.Create(model);
        }


        [HttpPut]
        public async Task<GenericResult> Update(TransactionUpdateModel model)
        {
            return await _transactionService.Update(model);
        }

        [HttpPut("approval")]
        public async Task<GenericResult> Update(Guid id)
        {
            return await _transactionService.Approval(id);
        }
        [HttpPut("reject")]
        public async Task<GenericResult> Reject(Guid id)
        {
            return await _transactionService.Reject(id);
        }

        [HttpDelete]
        public async Task<GenericResult> Delete(Guid id)
        {
            return await _transactionService.Delete(id);
        }
    }
}

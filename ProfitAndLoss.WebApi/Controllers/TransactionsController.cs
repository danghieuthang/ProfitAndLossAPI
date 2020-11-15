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
using ProfitAndLoss.Utilities;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Transaction.PREFIX)]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly ITransactionServices _transactionService;
        public TransactionsController(ITransactionServices transactionService, IdentityServices identityServices) : base(identityServices)
        {
            _transactionService = transactionService;
        }

        [HttpGet("all")]
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
        [HttpPost("search")]
        public async Task<GenericResult> Search([FromBody] TransactionSearchModel model)
        {
            return await _transactionService.Search(model);
        }

        [HttpPost]
        public async Task<GenericResult> Create([FromBody] TransactionCreateModel model)
        {
            var user = await _identityServices.GetUserByIdAsync(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return new GenericResult
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    ResultCode = AppResultCode.Unauthorized
                };
            }
            model.CreateMemberId = new Guid(user.Id);
            var validationModels = _transactionService.ValidateModel(model);
            if (validationModels.Count() > 0)
            {
                return new GenericResult()
                {
                    Message = EnumHelper.GetDisplayValue(AppResultCode.FailValidation),
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ResultCode = AppResultCode.FailValidation,
                    Data = validationModels
                };
            }
            /* validate relation ship object */
            return await _transactionService.Create(model);
        }


        [HttpPut]
        public async Task<GenericResult> Update(TransactionUpdateModel model)
        {
            return await _transactionService.Update(model);
        }

        [HttpPut("approval")]
        public async Task<GenericResult> Approval([FromForm] Guid id)
        {
            return await _transactionService.Approval(id);
        }
        [HttpPut("reject")]
        public async Task<GenericResult> Reject([FromForm] Guid id)
        {
            return await _transactionService.Reject(id);
        }

        [HttpDelete]
        public async Task<GenericResult> Delete(Guid id)
        {
            return await _transactionService.Delete(id);
        }

        /// <summary>
        /// Get all transaction detail by tranasction id
        /// </summary>
        /// <param name="id">The transaction id</param>
        /// <returns></returns>
        [HttpGet("{id}/transaction-details")]
        public async Task<GenericResult> GetTransactionDetailsByTransactionID(Guid id)
        {
            return await _transactionService.GetTransactionDetailByTranID(id);
        }
    }
}

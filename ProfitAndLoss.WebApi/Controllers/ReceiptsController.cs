﻿using System;
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
    [Route(RouteConstants.Receipt.PREFIX)]
    [ApiController]
    public class ReceiptsController : BaseController
    {
        private readonly IReceiptServices _receiptService;
        public ReceiptsController(IReceiptServices receiptService, IdentityServices identityServices) : base(identityServices)
        {
            _receiptService = receiptService;
        }

        /// <summary>
        /// Get all receipt
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<GenericResult> GetAll()
        {
            return await _receiptService.GetAll();
        }

        /// <summary>
        /// Get receipt by receip id
        /// </summary>
        /// <param name="id">The receipt id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GenericResult> GetById(Guid id)
        {
            return await _receiptService.GetById(id);
        }

        /// <summary>
        /// Search receipt
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> Search([FromQuery] ReceiptSearchModel model)
        {
            return await _receiptService.Search(model);
        }

        /// <summary>
        /// Create receipt
        /// </summary>
        /// <param name="model">The receipt model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> Create([FromBody] ReceiptCreateModel model)
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
            var validationModels = _receiptService.ValidateModel(model);
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
            return await _receiptService.Create(model);
        }


        /// <summary>
        /// Update receipt
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<GenericResult> Update(ReceiptUpdateModel model)
        {
            return await _receiptService.Update(model);
        }

        /// <summary>
        /// Approval receipt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("approval")]
        public async Task<GenericResult> Approval([FromForm] Guid id)
        {
            return await _receiptService.Approval(id);
        }

        /// <summary>
        /// Reject receipt
        /// </summary>
        /// <param name="id">The receipt Id</param>
        /// <returns></returns>
        [HttpPut("reject")]
        public async Task<GenericResult> Reject([FromForm] Guid id)
        {
            return await _receiptService.Reject(id);
        }

        /// <summary>
        /// Delete receipt
        /// </summary>
        /// <param name="id">The receipt id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<GenericResult> Delete(Guid id)
        {
            return await _receiptService.Delete(id);
        }

        /// <summary>
        /// Get all transaction detail by tranasction id
        /// </summary>
        /// <param name="id">The transaction id</param>
        /// <returns></returns>
        [HttpGet("{id}/transactions")]
        public async Task<GenericResult> GetTransactionsByReceiptId(Guid id)
        {
            return await _receiptService.GetTransactionsByReceiptId(id);
        }

        /// <summary>
        /// Get all evidence by receipID
        /// </summary>
        /// <param name="id">The receipt id</param>
        /// <returns></returns>
        [HttpGet("{id}/evidences")]
        public async Task<GenericResult> GetEvidencesByReceiptId(Guid id)
        {
            return await _receiptService.GetEvidencesByReceiptId(id);
        }
    }
}

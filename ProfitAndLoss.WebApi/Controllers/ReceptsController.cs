﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Receipt.PREFIX)]
    [ApiController]
    public class ReceptsController : BaseController
    {
        #region fields

        private readonly IReceiptServices _receptService;
        private readonly IStoreServices _storeService;

        #endregion fields


        public ReceptsController(IReceiptServices receptService, IStoreServices storeService, IdentityServices identityServices) : base(identityServices)
        {
            _receptService = receptService;
            _storeService = storeService;
        }

        /// <summary>
        /// Create new receipt
        /// </summary>
        /// <param name="model">The receipt create model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> Create([FromBody] ReceiptCreateModel model)
        {
            //if (_storeService.GetById(model.StoreId) == null)
            //{
            //    return new GenericResult
            //    {
            //        Data = model,
            //        Success = false,
            //        StatusCode = System.Net.HttpStatusCode.OK,
            //        Message = "Store not exists"
            //    };
            //}

            return await _receptService.Create(model);
        }

        /// <summary>
        /// Get all receipts
        /// </summary>
        /// <param name="model">The receipt search model</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> GetRecepts([FromQuery] ReceiptSearchModel model)
        {
            return await _receptService.SearchRecepts(model);
        }

        /// <summary>
        /// Get receipt by 
        /// </summary>
        /// <param name="id">The receipt id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GenericResult> GetReceiptById([FromRoute] Guid id)
        {
            return await _receptService.GetById(id);
        }
        /// <summary>
        /// Get receipt by transaction id
        /// </summary>
        /// <param name="id">The transaction id</param>
        /// <returns></returns>
        [HttpGet("search/{id}")]
        public async Task<GenericResult> GetReceiptByTransactionId([FromRoute] Guid id)
        {
            return await _receptService.GetReceiptByTransactionId(id);
        }

        /// <summary>
        /// Delete a receipts
        /// </summary>
        /// <param name="id">The receipt id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<GenericResult> DeleteReceipt(Guid id)
        {
            return await _receptService.Delete(id);
        }

        /// <summary>
        /// Update a receipt
        /// </summary>
        /// <param name="model">The receipt update model</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<GenericResult> UpdateReceipt([FromBody] ReceiptUpdateModel model)
        {
            return await _receptService.Update(model);
        }
    }
}

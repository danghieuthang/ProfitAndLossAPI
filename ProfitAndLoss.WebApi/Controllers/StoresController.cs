﻿using System;
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
    [Route(RouteConstants.Store.PREFIX)]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IBrandService _brandService;
        public StoresController(IStoreService storeService, IBrandService brandService)
        {
            _storeService = storeService;
            _brandService = brandService;
        }

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> GetAllStores()
        {
            return await _storeService.GetAll();
        }

        /// <summary>
        /// Get a store by id
        /// </summary>
        /// <param name="id">The store id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GenericResult> GetStoreById(Guid id)
        {
            return await _storeService.GetById(id);
        }


        /// <summary>
        /// Create store
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> CreateStore([FromBody] StoreCreateModel model)
        {
            var brand = _brandService.GetBrand(model.BrandId);
            if (brand.Result is null)
            {
                return new GenericResult
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            return await _storeService.Create(model);
        }

        /// <summary>
        /// Delete store 
        /// </summary>
        /// <param name="id">The store id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<GenericResult> DeleteStore(Guid id)
        {
            return await _storeService.Delete(id);
        }
    }
}

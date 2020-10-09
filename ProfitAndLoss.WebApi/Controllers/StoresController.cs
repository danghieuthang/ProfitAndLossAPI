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

        [HttpGet]
        public async Task<GenericResult> GetStores([FromQuery] RequestSearchStoreModel model)
        {
            return await _storeService.SearchStoreAsync(model);
        }

        [HttpPost]
        public async Task<GenericResult> CreateStore([FromBody] RequestCreateStoreModel model)
        {
            var brand = _brandService.GetBrand(model.BrandId);
            if (brand.Result is null)
            {
                return new GenericResult
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Error = "This brand not exists!"
                };
            }
            return await _storeService.Create(model);
        }


    }
}

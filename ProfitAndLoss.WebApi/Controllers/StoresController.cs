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
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [Route(RouteConstants.Store.GET)]
        public async Task<GenericResult> GetStores(RequestSearchStoreModel model)
        {
            return await _storeService.SearchStoreAsync(model);
        }

        [HttpGet]
        [Route(RouteConstants.Store.GET)]
        public async Task<GenericResult> CreateStore(RequestCreateStoreModel model)
        {
            return await _storeService.Create(model);
        }


    }
}

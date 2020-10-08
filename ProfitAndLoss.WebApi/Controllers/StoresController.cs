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
    [Route(RouteConstants.Store.PREFIX)]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<GenericResult> GetStores([FromQuery] RequestSearchStoreModel model)
        {
            return await _storeService.SearchStoreAsync(model);
        }

        [HttpPost]
        public async Task<GenericResult> CreateStore([FromBody] RequestCreateStoreModel model)
        {
            return await _storeService.Create(model);
        }


    }
}

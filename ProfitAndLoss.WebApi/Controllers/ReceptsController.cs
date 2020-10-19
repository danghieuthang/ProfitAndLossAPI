using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Receipt.PREFIX)]
    [ApiController]
    public class ReceptsController : ControllerBase
    {
        #region fields

        private readonly IReceiptService _receptService;
        private readonly IStoreService _storeService;

        #endregion fields

   
        public ReceptsController(IReceiptService receptService, IStoreService storeService)
        {
            _receptService = receptService;
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<GenericResult> Create([FromBody] ReceiptCreateModel model)
        {
            if (_storeService.GetById(model.StoreId) == null)
            {
                return new GenericResult
                {
                    Data = model,
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Store not exists"
                };
            }

            return await _receptService.Create(model);
        }

        [HttpGet]
        public async Task<GenericResult> GetRecepts([FromQuery] ReceiptSearchModel model)
        {
            return await _receptService.Search(model);
        }
    }
}

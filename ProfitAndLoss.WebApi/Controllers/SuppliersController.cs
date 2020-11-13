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
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Supplier.PREFIX)]
    [ApiController]
    public class SuppliersController : BaseController
    {
        private readonly ISupplierServices _supplierService;
        public SuppliersController(ISupplierServices supplierService, IdentityServices identityServices) : base(identityServices)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task<GenericResult> CreateSupplier([FromBody] SupplierCreateModel model)
        {
            return await _supplierService.Create(model);
        }

        [HttpGet]
        public async Task<GenericResult> GetAllSupplier()
        {
            return await _supplierService.GetAll();
        }
    }
}

using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IDemoExcelService _demoExcelService;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public BrandsController(IBrandService brandService, IDemoExcelService demoExcelService)
        {
            _brandService = brandService;
            _demoExcelService = demoExcelService;
        }


        [HttpPost]
        [Route(RouteConstants.Brand.CREATE)]
        public async Task<GenericResult> CreateBrand([FromBody] BrandCreateModel model)
        {
            return await _brandService.CreateBrand(model);
        }


        [HttpGet]
        [Route(RouteConstants.Brand.EXPORT)]

        public async Task<IActionResult> Export()
        {
            var file = _demoExcelService.ExportBrands();
            return File(file.Result, XlsxContentType, "Brands_" + DateTime.Now);

        }
    }
}

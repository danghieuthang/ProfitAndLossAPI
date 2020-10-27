using log4net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("error")]
    [ApiController]
    public class ErrorController : BaseController
    {
        private static ILog _logger;
        public ErrorController()
        {
            _logger = this.Log();
        }
        [Route("")]
        public GenericResult HandleException()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context.Error == null) return new GenericResult() { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            var e = context.Error;
            _logger.Error(e);
            return Error(e.Message);
        }
    }
}

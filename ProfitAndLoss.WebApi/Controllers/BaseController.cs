using AutoMapper;
using ProfitAndLoss.WebApi.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using ProfitAndLoss.Utilities;
using Microsoft.AspNetCore.Identity;
using ProfitAndLoss.Data.Models;
using ServiceStack;
using ProfitAndLoss.Business.Services;

namespace ProfitAndLoss.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IdentityServices _identityServices;

        public BaseController(IdentityServices identityServices)
        {
            _identityServices = identityServices;
        }

        protected GenericResult Error(object obj = default)
        {
            return new GenericResult()
            {
                Data = obj,
                Message = EnumHelper.GetDisplayValue(AppResultCode.FailValidation),
                ResultCode = AppResultCode.FailValidation,
                Success = false,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}

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

namespace ProfitAndLoss.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
        public string UserId
        {
            get
            {
                return User.Identity.Name;
            }
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

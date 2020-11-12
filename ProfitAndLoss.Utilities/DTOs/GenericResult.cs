using Newtonsoft.Json;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ProfitAndLoss.Utilities.DTOs
{
    public class GenericResult
    {
        #region constructors

        public GenericResult()
        {
            Data = new object();
            Error = new object();
            Success = true;
            Message = string.Empty;
        }

        public GenericResult(Object data, bool success, string message = null)
        {
            Data = data;
            Success = success;
            Message = message??EnumHelper.GetDisplayValue(AppResultCode.Success);
        }
        #endregion constructors

        #region properties
        [JsonProperty("results")]
        public Object Data { get; set; }
        public Object Error { get; set; }
        
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public AppResultCode? ResultCode { get; set; }
        #endregion properties
    }
}

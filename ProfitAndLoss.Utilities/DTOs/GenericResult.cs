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
        
        [JsonProperty("success")]
        public bool Success { get; set; }
       
        //[JsonProperty("status_code")]
        //public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public AppResultCode? ResultCode { get; set; }

        

        #endregion properties
    }
}

using System;
using System.Collections.Generic;
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
            Error = new object();
        }

        public GenericResult(Object data, bool success, string message, Object error)
        {
            Data = data;
            Success = success;
            Message = message;
            Error = error;
        }
        #endregion constructors

        #region properties

        public Object Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public Object Error { get; set; }

        #endregion properties

    }
}

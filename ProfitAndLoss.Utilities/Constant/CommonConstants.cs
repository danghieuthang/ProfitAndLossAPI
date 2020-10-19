using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.Constant
{
    public static class CommonConstants
    {
        public const int DEFAULT_PAGESIZE = 5;
        /// <summary>
        /// Receipt status
        /// </summary>
        public static class ReceiptStatus
        {
            public const int CREATED = 1;
            public const int PROCESSING = 2;
            public const int APPROVAL = 3;
        }
    }

   
}
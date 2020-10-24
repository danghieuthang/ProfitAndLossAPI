using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.Constant
{
    public static class CommonConstants
    {
        public const int DEFAULT_PAGESIZE = 10;
        /// <summary>
        /// Receipt status
        /// </summary>
        public static class TransactionStatus
        {
            public const int NEW = 1;
            public const int APPROVAL = 2;
            public const int REJECT = 3;
        }
    }

   
}
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
       
    }
    public enum ReceiptStatus
    {
        NEW = 1,
        APPROVED,
        REJECTED,
        REMOVED,
        SPLITED
    }


}
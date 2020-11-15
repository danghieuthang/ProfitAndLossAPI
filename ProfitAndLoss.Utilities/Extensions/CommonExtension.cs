using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.Extensions
{
    public static class CommonExtension
    {
        public static string ToPercent(this double percent)
        {
            return (percent*100).ToString("00.##") + " %";
        }
    }
}

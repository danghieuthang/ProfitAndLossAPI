using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.DTOs
{
    public class PageResult<T> : PageResultBase where T : class
    {
        #region constructors

        public PageResult()
        {
            Results = new List<T>();
        }

        #endregion constructors

        #region properties

        public IList Results { get; set; }

        #endregion properties

    }
}

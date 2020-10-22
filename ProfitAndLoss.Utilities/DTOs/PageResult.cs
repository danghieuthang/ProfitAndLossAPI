using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.DTOs
{
    public class PageResult<T> where T : class
    {
        #region constructors

        public PageResult()
        {
            Results = new List<T>();
        }

        #endregion constructors

        #region properties

        /// <summary>
        /// List items of page
        /// </summary>
        [JsonProperty("data")]
        public IList Results { get; set; }

        /// <summary>
        /// Current Page
        /// </summary>
        [JsonProperty("current")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Total Row
        /// </summary>
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

       [JsonProperty("totalPage")]
        public int TotalPage { get ; set; }

        #endregion properties

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.DTOs
{
    public abstract class PageResultBase
    {
        /// <summary>
        /// Current Page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Number Row Of Page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total Row
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// First row of page
        /// </summary>
        public int FirstRow => (CurrentPage - 1) * PageSize + 1;

        /// <summary>
        /// Last row of page
        /// </summary>
        public int LastRow => Math.Min(CurrentPage * PageSize, RowCount);

        /// <summary>
        /// Number page
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(RowCount / (PageSize * 1.0));
            }
            set
            {
                PageCount = value;
            }
        }

    }
}

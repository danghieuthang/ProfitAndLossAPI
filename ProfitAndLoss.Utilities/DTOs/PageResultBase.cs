using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.DTOs
{
    public abstract class PageResultBase
    {
        // Current Page
        public int CurrentPage { get; set; }

        //Number Row of Page
        public int PageSize { get; set; }

        //Total Row
        public int RowCount { get; set; }

        //First Row of Page
        public int FirstRow => (CurrentPage - 1) * PageSize + 1;

        //Last Row of Page
        public int LastRow => Math.Min(CurrentPage * PageSize, RowCount);

        //Number Page
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

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class BaseSearchModel<T> : Mapping<T>
    {
        public BaseSearchModel()
        {

        }

        public Guid Id { get; set; }

        public DateTime TransactionDateFrom { get; set; }

        public DateTime TransactionDateTo { get; set; }
    }

    public class BaseUpdateModel<T> : Mapping<T>
    {
        public BaseUpdateModel()
        {

        }

        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public class BaseCreateModel<T> : Mapping<T>
    {
        public BaseCreateModel()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }



}

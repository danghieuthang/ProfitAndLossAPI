using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Actived { get; set; }
    }
}

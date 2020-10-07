using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Categories")]
    public class Category : BaseEntity<Guid>
    {
        public Category()
        {

        }

        [ForeignKey("Category")]
        public Guid ParentId { get; set; }

        public virtual ICollection<TransactionDetail> LedgerEntries { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            Actived = false;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("Category")]
        public Guid ParentId { get; set; }

        public bool Actived { get; set; }

        public virtual ICollection<TransactionDetail> LedgerEntries { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }

    }
}

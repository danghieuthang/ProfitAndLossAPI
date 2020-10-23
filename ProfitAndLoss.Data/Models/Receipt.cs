using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Receipts")]
    public class Receipt : BaseEntity<Guid>
    {
        public Receipt()
        {

        }

        [ForeignKey("Member")]
        public Guid CreateMemberId { get; set; }
        public Member Member { get; set; }

        public string Description { get; set; }

        [ForeignKey("ReceiptType")]
        public Guid TypeId { get; set; }
        public ReceiptType ReceiptType { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [ForeignKey("Store")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public decimal Price { get; set; }

        public int Status { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Evidence> Evidences { get; set; }      

    }
}

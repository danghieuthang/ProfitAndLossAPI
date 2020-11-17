using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Evidences = new HashSet<Evidence>();
        }

        [MaxLength(255)]
        public string Code { get; set; }

        [ForeignKey("Store")]
        public Guid? StoreId { get; set; }

        public virtual Store Store { get; set; }

        [ForeignKey("Member")]
        public Guid? CreateMemberId { get; set; }

        public virtual Member Member { get; set; }

        [ForeignKey("ReceiptType")]
        public Guid? ReceiptTypeId { get; set; }

        public virtual ReceiptType ReceiptType { get; set; }

        [DefaultValue(0)]
        public double TotalBalance { get; set; }

        [DefaultValue(0)]
        public double ShippingFee { get; set; }

        [DefaultValue(0)]
        public double DiscountPercent { get; set; }

        [DefaultValue(0)]
        public double DiscountValue { get; set; }

        [DefaultValue(0)]
        public double SubTotal { get; set; }

        public double AmountPaid { get; set; }

        public double DueBalance { get; set; }

        public DateTime TermExport { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime CloseDate { get; set; }

        public string NoteMessage { get; set; }

        [ForeignKey("Supplier")]
        public Guid? SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public int Status { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<ReceiptHistory> ReceiptHistories { get; set; }

        public virtual ICollection<Evidence> Evidences { get; set; }


    }
}

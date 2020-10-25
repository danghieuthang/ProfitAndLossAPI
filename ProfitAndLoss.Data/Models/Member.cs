using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Members")]
    public class Member : BaseEntity<Guid>
    {
        public Member()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [ForeignKey("Store")]
        public Guid? StoreId { get; set; }
        public virtual Store Store { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Members")]
    public class Member
    {
        public Member()
        {
            Transactions = new HashSet<Transaction>();
            MemberStores = new HashSet<MemberStore>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<MemberStore> MemberStores { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

    }
}

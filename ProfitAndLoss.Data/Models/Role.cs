using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Roles")]
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string RoleName { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("Evidences")]
    public class Evidence
    {
        public Evidence()
        {
            Actived = false;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ImgUrl { get; set; }

        [ForeignKey("Recept")]
        public Guid ReceptId { get; set; }

        public bool Actived { get; set; }
    }
}

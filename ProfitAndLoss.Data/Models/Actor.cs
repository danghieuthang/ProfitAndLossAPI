using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfitAndLoss.Data.Models
{
    public class Actor:BaseEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

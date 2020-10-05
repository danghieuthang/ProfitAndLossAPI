using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    [Table("MemberStores")]
    public class MemberStore
    {
        public MemberStore()
        {

        }
        public Guid MemberId { get; set; }

        public Member Member { get; set; }

        public Guid StoreId { get; set; }

        public Store Store { get; set; }
    }
}

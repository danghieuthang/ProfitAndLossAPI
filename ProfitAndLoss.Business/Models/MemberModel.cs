using ProfitAndLoss.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProfitAndLoss.Business.Models
{
    public class MemberCreateModel : BaseCreateModel<Member>
    {
        public MemberCreateModel()
        {

        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class MemberSearchModel : BaseSearchModel<Member>
    {
        public MemberSearchModel ()
        {

        }

        public string Name { get; set; }
    }

    public class MemberUpdateModel : BaseUpdateModel<Member>
    {
        public MemberUpdateModel()
        {

        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class MemberLoginModel : Mapping<Member>
    {
        public string UserName { get; set; }
    }

}

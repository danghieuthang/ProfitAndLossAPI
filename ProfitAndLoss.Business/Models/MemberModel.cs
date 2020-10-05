using ProfitAndLoss.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateMemberModel : BaseCreateModel<Member>
    {
        public RequestCreateMemberModel()
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

    public class RequestSearchMemberModel: BaseSearchModel<Member>
    {
        public RequestSearchMemberModel()
        {

        }

        public string Name { get; set; }
    }

    public class RequestUpdateMemberModel : BaseUpdateModel<Member>
    {
        public RequestUpdateMemberModel()
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

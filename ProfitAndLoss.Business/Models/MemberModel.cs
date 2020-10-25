using Newtonsoft.Json;
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
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public Guid? StoreId { get; set; }
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

    public class MemberViewModel : BaseViewModel<Member>
    {
        public MemberViewModel()
        {

        }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("first-name")]
        public string FirstName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

}

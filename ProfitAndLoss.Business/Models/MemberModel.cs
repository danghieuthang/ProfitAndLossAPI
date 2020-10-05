using ProfitAndLoss.Data.Models;

namespace ProfitAndLoss.Business.Models
{
    public class MemberCreateModel
    {
        public string UserName{ get; set; }
        public string Email { get; set; }
    }
    public class MemberLoginModel : Mapping<Member>
    {
        public string UserName { get; set; }
    }

}

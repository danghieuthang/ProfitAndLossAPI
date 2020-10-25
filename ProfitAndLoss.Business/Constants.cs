using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business
{
    public class JWT
    {
        public const string ISSUER = "efcoredemo";
        public const string AUDIENCE = "efcoredemo";
        public const string SECRET_KEY = "ASDFOIPJJP812340-89ADSFPOUADSFH809-3152-798OHASDFHPOU1324-8ASDF";
    }
    public class RoleName
    {
        public const string ADMIN = "Admin";
        public const string INVESTOR = "Investor";
        public const string CHIEF_ACCOUNTANT = "ChiefAccountant";
        public const string MEMBER_IN_STORE = "MemberInStore";
    }
}

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
    public class ConnectionString
    {
        public const string CNN = @"Server=115.165.166.32;Database=SWD_ProfitAndLoss;Trusted_Connection=False;User Id=Superuser;Password=ZNa1R3MTG15BdnYpX1CZ5MuO6IyNeA0+jaaAViYiK09h9TqimhNKC3VEh6yhjH5h;MultipleActiveResultSets=true";
    }
    public class LoginRequestType
    {
        public const string LOCAL_USER = "default";
        public const string FIREBASE_USER = "firebase";
    }
    public class RoleName
    {
        public const string ADMIN = "Admin";
        public const string INVESTOR = "Investor";
        public const string CHIEF_ACCOUNTANT = "ChiefAccountant";
        public const string MEMBER_IN_STORE = "MemberInStore";

        public static IDictionary<int, string> USER_ROLE = new Dictionary<int, string>(){
            {1, ADMIN },
            {2, INVESTOR },
            {3, CHIEF_ACCOUNTANT },
            {4, MEMBER_IN_STORE }
        };

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi
{
    public class ApiVer1UrlConstant
    {
        public static class Member
        {
            private const string PREFIX = "member";
            public const string LOGIN = PREFIX + "/login";

        }

        public static class Brand
        {
            private const string PREFIX = "brand";

            public const string CREATE = PREFIX + "/create";
        }

        public static class User
        {
            private const string PREFIX = "users";
            public const string LOGIN = PREFIX + "/login";
            public const string CREATE = PREFIX;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi
{
    public class RouteConstants
    {
        public static class Member
        {
            private const string PREFIX = "members";
            public const string LOGIN = PREFIX + "/login";

        }

        public static class Brand
        {
            private const string PREFIX = "brands";

            public const string CREATE = PREFIX + "/create";
        }

        public static class User
        {
            public const string PREFIX = "users";
            public const string LOGIN = PREFIX + "/login";
            public const string CREATE = PREFIX;
        }

        public static class Store
        {
            public const string PREFIX = "stores";
            public const string GET = PREFIX;
        }
    }
}

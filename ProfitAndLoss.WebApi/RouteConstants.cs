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
            private const string PREFIX = "api/members";
            public const string LOGIN = PREFIX + "/login";

        }

        public static class Brand
        {
            private const string PREFIX = "brands";

            public const string CREATE = PREFIX + "/create";
            public const string EXPORT = PREFIX + "/export";

        }

        public static class User
        {
            public const string PREFIX = "api/users";
            public const string LOGIN = PREFIX + "/login";
            public const string CREATE = PREFIX;
        }

        public static class Store
        {
            public const string PREFIX = "stores";
            public const string GET = PREFIX;
            public const string POST = PREFIX;

        }

        public static class Recept
        {
            public const string PREFIX = "api/recepts";
        }

        public static class Evidence
        {
            public const string PREFIX = "api/evidences";
        }
    }
}

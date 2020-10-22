using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi
{
    public class RouteConstants
    {
        public const string VERSION = "V01";
        public static class Member
        {
            private const string PREFIX = "api/"+VERSION+"/members";
            public const string LOGIN = PREFIX + "/login";

        }

        public static class Brand
        {
            private const string PREFIX = "api/" + VERSION + "/brands";

            public const string CREATE = PREFIX + "/create";
            public const string EXPORT = PREFIX + "/export";

        }

        public static class User
        {
            public const string PREFIX = "api/" + VERSION + "/users";
            public const string LOGIN = PREFIX + "/login";
            public const string CREATE = PREFIX;
        }

        public static class Store
        {
            public const string PREFIX = "api/" + VERSION + "/stores";
            public const string GET = PREFIX;
            public const string POST = PREFIX;

        }

        public static class Receipt
        {
            public const string PREFIX = "api/" + VERSION + "/receipts";
        }

        public static class Evidence
        {
            public const string PREFIX = "api/" + VERSION + "/evidences";
        }

        public static class ReceiptType
        {
            public const string PREFIX = "api/" + VERSION + "/receipttypes";
        }

        public static class Supplier
        {
            public const string PREFIX = "api/" + VERSION + "/suppliers";
        }
    }
}

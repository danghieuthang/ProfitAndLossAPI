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

        public static class Receipt
        {
            public const string PREFIX = "api/receipts";
        }

        public static class Evidence
        {
            public const string PREFIX = "api/evidences";
        }

        public static class ReceiptType
        {
            public const string PREFIX = "api/receipttypes";
        }

        public static class Supplier
        {
            public const string PREFIX = "api/supplier";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi
{
    public class RouteConstants
    {
        public const string VERSION = "v1";
        public static class Member
        {
            public const string PREFIX = "api/" + VERSION + "/members";
            public const string LOGIN = PREFIX + "/login";

        }

        public static class Brand
        {
            public const string PREFIX = "api/" + VERSION + "/brands";

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
            public const string PREFIX = "api/" + VERSION + "/receipt-types";
        }

        public static class Supplier
        {
            public const string PREFIX = "api/" + VERSION + "/suppliers";
        }

        public static class AccountingPeriod
        {
            public const string PREFIX = "api/" + VERSION + "/accounting-periods";
        }
    }
}

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
        public const string CNN = @"SQL5097.site4now.net;Initial Catalog=DB_A69FBF_dhthang1998;User Id=DB_A69FBF_dhthang1998_admin;Password=thang1171998";
    }
    public class LoginRequestType
    {
        public const string LOCAL_USER = "default";
        public const string FIREBASE_USER = "firebase";
    }
    public static class RoleName
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

    public static class TransactionTypeCode
    {
        public const string EXPENSE = "EXP";
        public const string REVENUES = "REV";
        public const string INVOICE = "INV";
        public const string SALES = "SAL";
    }

    public static class TransactionCategoryCode
    {
        public const string COST_OF_GOOGS_SOLD = "SAL-COGS";
    }

    public static class AccountingPeriodStatus
    {
        public const int OPEN = 1;
        public const int CLOSED = 2;
        public const int CANCEL = 3;
    }

    public static class FirebaseAuthenInfo
    {
        public static readonly string ApiKey = "AIzaSyB0bAvWYtuR-EP0YiultKtT2yhdW40HgMw";
        public static readonly string Bucket = "swdk13.appspot.com";
        public static readonly string AuthEmail = "dhthang1998@gmail.com";
        public static readonly string AuthPassword = "anhthangdepZai123";
        public static readonly string Sercet = "tDFrTYuOxMDZnkZ8L4S3GO0TeLwJBbBCETy79bqu";

    }
    public static class TransactionStatus
    {
        public static readonly int PROCESS = 0;
        public static readonly int APPROVAL = 1;
    }
}
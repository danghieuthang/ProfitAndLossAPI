using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirebaseToken { get; set; }
        public string RequestType { get; set; }
        public bool RememberMe { get; set; }
    }
    public class TokenResponseLoginModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public StoreViewModel Store { get; set; }
    }

    public class UserCreateModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}

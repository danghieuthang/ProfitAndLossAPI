using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class TokenResponseLoginModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
    }

    public class RequestCreateModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}

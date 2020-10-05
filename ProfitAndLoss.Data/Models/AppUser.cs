using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Data.Models
{
    public class AppUser : IdentityUser<string>
    {
        public string Fullname { get; set; }
    }
}

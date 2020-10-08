using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProfitAndLoss.Utilities
{
    public enum AppResultCode
    {
        [Display(Name = "Unknown error")]
        UnknownError = 1,
        [Display(Name = "Success")]
        Success = 2,
        [Display(Name = "Fail validation")]
        FailValidation = 3,
        [Display(Name = "Not found")]
        NotFound = 4,
        [Display(Name = "Unsupported")]
        Unsupported = 5,
        [Display(Name = "Can not delete because of dependencies")]
        DependencyDeleteFail = 6,
        [Display(Name = "Unauthorized")]
        Unauthorized = 7,
        [Display(Name = "Username has already existed")]
        DuplicatedUsername = 8

    }
}

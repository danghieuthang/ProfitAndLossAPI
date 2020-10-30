using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfitAndLoss.Business;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using ProfitAndLoss.Utilities;
using FirebaseAdmin.Auth;

namespace ProfitAndLoss.WebApi.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IdentityServices _identityServices;
        public UsersController(IdentityServices identityServices)
        {
            _identityServices = identityServices;
        }
        /// <summary>
        /// Using for login an account 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>
        /// Token object
        /// </returns>
        [HttpPost]
        [Route(RouteConstants.User.LOGIN)]
        [AllowAnonymous]
        //[Route(ApiVer1UrlConstant.User.LOGIN)]
        public async Task<GenericResult> LoginAsync([FromBody] RequestLoginModel login)
        {
            List<ValidationModel> listValidation = new List<ValidationModel>();
            AppUser appUser = new AppUser();
            var response = new TokenResponseLoginModel();
            switch (login.RequestType)
            {
                case LoginRequestType.FIREBASE_USER:
                    FirebaseToken validResult = await _identityServices.ValidateFirebaseToken(login.FirebaseToken);
                    if (validResult == null)
                    {
                        listValidation.Add(new ValidationModel()
                        {
                            Data = StringHelpers.HyphensCase(nameof(login.Username)),
                            Message = "Invalid username"
                        });
                    }
                    UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(validResult.Uid);

                    appUser = await _identityServices.GetUserByUserNameAsync(userRecord.Uid);
                    if (appUser == null)
                    {
                        // get user infors from firebase
                        appUser = _identityServices.ConvertToUser(userRecord);
                        // create new User
                        var resultCreate = await _identityServices
                                .CreateUserWithoutPassAsync(appUser);
                        /* regist new user */
                        if (resultCreate == null)
                        {
                            listValidation.Add(new ValidationModel()
                            {
                                Data = StringHelpers.HyphensCase(nameof(login.Username)),
                                Message = "Can not check user name"
                            });
                        }
                        appUser = await _identityServices.GetUserByUserNameAsync(userRecord.Uid);
                    }
                    break;
                case LoginRequestType.LOCAL_USER:
                default:
                    if (!string.IsNullOrEmpty(login.Password))
                    {
                        // login with local user
                        appUser = await _identityServices.GetUserByUserNameAsync(login.Username);
                        if (appUser == null)
                        {
                            listValidation.Add(new ValidationModel()
                            {
                                Data = StringHelpers.HyphensCase(nameof(login.Username)),
                                Message = "Invalid username"
                            });
                        }
                        var result = await _identityServices.SignInAsync(appUser, login);
                        if (!result.Succeeded)
                        {
                            listValidation.Add(new ValidationModel()
                            {
                                Data = StringHelpers.HyphensCase(nameof(login.Password)),
                                Message = "Invalid password"
                            });
                        }
                    }
                    else
                    {
                        listValidation.Add(new ValidationModel()
                        {
                            Data = StringHelpers.HyphensCase(nameof(login.Password)),
                            Message = "Invalid password"
                        });
                    }
                    break;
            }
            if (listValidation.Count > 0)
            {
                return new GenericResult
                {
                    Success = false,
                    Error = listValidation,
                    Message = EnumHelper.GetDisplayValue(AppResultCode.FailValidation),
                    ResultCode = AppResultCode.FailValidation
                };
            }
            var tokenString = await _identityServices.GenerateJWTTokenAsync(appUser);
            var listRole = _identityServices.GetRole(appUser);

            response = new TokenResponseLoginModel()
            {
                UserId = appUser.Id,
                Username = appUser.UserName,
                AccessToken = tokenString,
                Role = listRole == null ? "" : listRole.Result.FirstOrDefault()
            };
            return new GenericResult { Data = response, Success = true };
        }
        //Member AuthenticateMember(Member loginCredentials)
        //{
        //    Member Member = appMembers.SingleOrDefault(x => x.MemberName == loginCredentials.MemberName && x.Password == loginCredentials.Password);
        //    return Member;
        //}


        [HttpPost]
        [Route(RouteConstants.User.PREFIX)]
        public async Task<GenericResult> CreateUserAsync([FromBody] RequestCreateModel model)
        {
            AppUser user = new AppUser()
            {
                UserName = model.Username,
                Fullname = model.FullName
            };
            var result = await _identityServices.CreateUserWithDefaultRoleAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new GenericResult { Success = true };
            }
            return new GenericResult { Success = false, Data = result.Errors };
        }


        [HttpPost(RouteConstants.Role.INIT)]
        public async Task<GenericResult> CreateAllRoleAsync()
        {
            var listRole = new List<string>()
            {
                RoleName.ADMIN,
                RoleName.CHIEF_ACCOUNTANT,
                RoleName.INVESTOR,
                RoleName.MEMBER_IN_STORE
            };
            await _identityServices.CreateRoles(listRole);
            return new GenericResult { Success = false };
        }
    }
}

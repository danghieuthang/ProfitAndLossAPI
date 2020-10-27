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

namespace ProfitAndLoss.WebApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
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
            var appUser = await _identityServices.GetUserByUserNameAsync(login.Username);
            if (appUser == null)
            {
                return new GenericResult { Success = false, Data = Unauthorized("Invalid username") };
            }
            var result = await _identityServices.SignInAsync(appUser, login);
            if (!result.Succeeded)
            {
                return new GenericResult { Success = false, Data = Unauthorized("Invalid password") };
            }
            var tokenString = await _identityServices.GenerateJWTTokenAsync(appUser);
            var listRole = _identityServices.GetRole(appUser);

            var response = new TokenResponseLoginModel()
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

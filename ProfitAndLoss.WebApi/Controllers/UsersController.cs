using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ProfitAndLoss.Business;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static ServiceStack.Script.Lisp;
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
            //if (ModelState.IsValid)
            //{

            //}
            //await _identityServices.CreateRoles();
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
            var result = await _identityServices.CreateUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new GenericResult { Success = true };
            }
            return new GenericResult { Success = false, Data = result.Errors };
        }
    }
}

using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProfitAndLoss.Business.Services
{
    public class IdentityServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly DataContext _context;
        private readonly IMemberServices _memberServices;
        public IdentityServices(UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager,
                                  RoleManager<AppRole> roleManager, DataContext context,
                                   IMemberServices memberServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _memberServices = memberServices;
        }
        protected void PrepareCreate(AppUser entity, string newGuid = "")
        {
            var guid = !string.IsNullOrEmpty(newGuid) ? newGuid : Guid.NewGuid().ToString();
            entity.Id = Guid.NewGuid().ToString();
        }
        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            PrepareCreate(user);
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var memberModel = new MemberCreateModel()
                {
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    FirstName = user.Fullname,
                    UserName = user.UserName
                };
                await _memberServices.CreateMemberAsync(memberModel);
            }
            return result;
        }
        public async Task<IdentityResult> CreateUserWithDefaultRoleAsync(AppUser user, string password)
        {
            PrepareCreate(user);
            var listRole = new List<string>() { RoleName.MEMBER_IN_STORE };
            await CreateRoles(listRole);
            var result = await _userManager.CreateAsync(user, password);
            if (listRole.Count != 0)
            {
                await _userManager.AddToRolesAsync(user, listRole);
            }
            if (result.Succeeded)
            {
                var memberModel = new MemberCreateModel()
                {
                    Id = new Guid(user.Id),
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    FirstName = user.Fullname,
                    UserName = user.UserName
                };
                await _memberServices.CreateMemberAsync(memberModel);
            }
            return result;
        }


        public async Task CreateRoles(List<string> roles)
        {
            IdentityResult roleResult = new IdentityResult();
            //Adding Admin Role
            foreach (var role in roles)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    var roleCheck = await _roleManager.RoleExistsAsync(role);
                    if (!roleCheck)
                    {
                        AppRole appRole = new AppRole(role);
                        PrepareCreate(appRole);
                        roleResult = await _roleManager.CreateAsync(appRole);
                    }
                }
            }
            _context.SaveChanges();
        }
        public async Task<SignInResult> SignInAsync(AppUser appUser, RequestLoginModel model)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(appUser,
                password: model.Password, false);
            return result;
        }
        public async Task<ClaimsIdentity> GetIdentityAsync(AppUser entity, string scheme)
        {
            var identity = new ClaimsIdentity(scheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, entity.Id.ToString()));
            var claims = await _userManager.GetClaimsAsync(entity);
            var roles = await _userManager.GetRolesAsync(entity);
            foreach (var r in roles)
                claims.Add(new Claim(ClaimTypes.Role, r));
            identity.AddClaims(claims);
            return identity;
        }
        public async Task<string> GenerateJWTTokenAsync(AppUser MemberInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var ident = await GetIdentityAsync(MemberInfo, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(ident);
            var key = Encoding.Default.GetBytes(JWT.SECRET_KEY);
            var issuer = JWT.ISSUER;
            var audience = JWT.AUDIENCE;
            var identity = principal.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, principal.Identity.Name));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = identity,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(key),
                   SecurityAlgorithms.HmacSha256Signature),
                NotBefore = DateTime.Now
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        protected void PrepareCreate(AppRole entity)
        {
            entity.Id = Guid.NewGuid().ToString();
        }
        public async Task<IList<string>> GetRole(AppUser appUser)
        {
            var user = await _userManager.GetRolesAsync(appUser);
            return user;
        }

        public async Task<FirebaseToken> ValidateFirebaseToken(string tokenStr)
        {
            FirebaseToken decodedToken = null;
            try
            {
                decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(tokenStr);
            }
            catch (FirebaseAuthException ex)
            {

            }
            return decodedToken;
        }
        public AppUser ConvertToUser(UserRecord firebaseUser)
        {
            var entity = new AppUser
            {
                UserName = firebaseUser.Uid,
                Fullname = firebaseUser.DisplayName,
                Email = firebaseUser.Email,
                EmailConfirmed = firebaseUser.EmailVerified,
                PhoneNumber = firebaseUser.PhoneNumber
            };
            return entity;
        }
        public async Task<IdentityResult> CreateUserWithoutPassAsync(AppUser entity)
        {
            if (!string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = new Guid(entity.Id).ToString();
            }
            var result = await CreateUserWithDefaultRoleAsync(entity, "ReadlyStrongPassword123");
            return result;
        }

        /// <summary>
        /// Get All Email of user has role is Investor
        /// </summary>
        /// <returns>List emails</returns>
        public async Task<List<string>> GetAllInvestor()
        {
            return _userManager.GetUsersInRoleAsync(RoleName.INVESTOR).Result.Select(x => x.Email).ToList();
        }
    }
}

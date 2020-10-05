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

namespace ProfitAndLoss.Business.Services
{
    public class IdentityServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly DataContext _context;
        public IdentityServices(UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager,
                                  RoleManager<AppRole> roleManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        protected void PrepareCreate(AppUser entity)
        {
            entity.Id = Guid.NewGuid().ToString();
        }
        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
               var  user = await  _userManager.FindByNameAsync(username);
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
            return  await _userManager.CreateAsync(user, password);
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
            identity.AddClaim(new Claim(ClaimTypes.Name, entity.Id));
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
        public async Task CreateRoles()
        {
            IdentityResult roleResult = new IdentityResult();
            //Adding Admin Role
            var roleCheck = await _roleManager.RoleExistsAsync(RoleName.ADMIN);
            if (!roleCheck)
            {
                AppRole role = new AppRole(RoleName.ADMIN);
                PrepareCreate(role);
                roleResult = await _roleManager.CreateAsync(role);
            }
            //Adding User Role
            roleCheck = await _roleManager.RoleExistsAsync(RoleName.USER);
            if (!roleCheck)
            {
                AppRole role = new AppRole(RoleName.USER);
                PrepareCreate(role);
                roleResult = await _roleManager.CreateAsync(role);
            }
            _context.SaveChanges();
        }
        public async Task<IList<string>> GetRole(AppUser appUser)
        {
            var user = await _userManager.GetRolesAsync(appUser);
            return user;
        }

    }
}

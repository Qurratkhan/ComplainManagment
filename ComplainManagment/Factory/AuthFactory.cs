using ComplainManagment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace ComplainManagment.Factory
{
    public class AuthFactory : IAuthFactory
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthFactory(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            throw new NotImplementedException();
        }

        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<ResponseApi<object>> Login(Login body)
        {
            var user = await _userManager.FindByNameAsync(body.Username);
            if(user != null && await _userManager.CheckPasswordAsync(user, body.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
                };

                foreach(var userRole in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaim);
                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Ok",
                    Response = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }


                };
            }
            return new()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized access",
                Response = body
            };
        }
        #endregion

        public Task<ResponseApi<object>> Register(Registration body)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi<object>> RegisterAdmin(Registration body)
        {
            throw new NotImplementedException();
        }
    }
}

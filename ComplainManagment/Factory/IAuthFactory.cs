using ComplainManagment.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ComplainManagment.Factory
{
    public interface IAuthFactory
    {
        Task<ResponseApi<object>> Login(Login body);
        Task<ResponseApi<object>> Register(Registration body);
        Task<ResponseApi<object>> RegisterAdmin(Registration body);
        JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}

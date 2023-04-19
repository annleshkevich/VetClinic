using System.IdentityModel.Tokens.Jwt;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateJwtToken(User user);
        string GetToken(User user);
    }
}

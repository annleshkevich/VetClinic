using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _key = AuthOptions.GetSymmetricSecurityKey();
        }
        public JwtSecurityToken GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, user.Login),
             new Claim(ClaimTypes.Role, user.Role.Name.ToString())};
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds,
                notBefore: DateTime.UtcNow);
            return tokenDescriptor;
        }
        public string GetToken(User user)
        {
            var jwtToken = GenerateJwtToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.WriteToken(jwtToken);
            return encodedJwt;
        }
    }
}

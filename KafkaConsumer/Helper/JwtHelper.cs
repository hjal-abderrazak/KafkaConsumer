using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KafkaConsumer.Helper
{
    public class JwtHelper
    {
        private readonly IConfiguration configuration;
        private readonly string secureKey, audiance, issuer;
        public JwtHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
             secureKey = configuration["JwtConfig:Secret"];
             audiance = configuration["JwtConfig:Audience"];
             issuer = configuration["JwtConfig:Audience"];
        }
        public string generate(User user)
        {
           
            var claims = new[]
          {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentals = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentals);
            var payload = new JwtPayload(null, null, claims, null, DateTime.Today.AddDays(7));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
         
        }



        public JwtSecurityToken Verify(String jwt)
        {
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                //ValidAudience = audiance,
                //ValidIssuer = issuer
                
                
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
    }
}

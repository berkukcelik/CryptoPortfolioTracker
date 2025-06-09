using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoPortfolioTrackerAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace CryptoPortolioTrackerAPI.Services
{
    public class TokenGenerateService : ITokenGenerateService
    {
        private readonly IConfiguration _configuration;

        public TokenGenerateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(User user)
        {
            // jwt token üç parçadan oluşur 
            // header -> türü ve imzalama algoritmasını belirtir
            // payload(claims) -> token içerisine kullanıcı bilgilerini yüklemek için
            // signature -> jwt secret key ile imzalama 
            // header kısmı için : 
 // Possible null reference argument.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
 // Possible null reference argument.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // payload kısmı için :
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Username),
                new Claim(ClaimTypes.Email , user.Email)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Kullanıcı bilgileri
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])), // Geçerlilik süresi
                Issuer = _configuration["Jwt:Issuer"], // Token'ı veren
                Audience = _configuration["Jwt:Audience"], // Token'ı alacak hedef kitle
                SigningCredentials = credentials // İmzalama bilgileri
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
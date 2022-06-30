using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(
            string tenantId,
            string accessId,
            string email,
            string name,
            string[] roles,
            string changePassword)
        {
            // lib que gera o token
            JwtSecurityTokenHandler tokenHandler = new();
            // pega o array de bytes do token
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenConfiguration:key").Value);

            IList<Claim> claimCollection = new List<Claim>();
            foreach (var role in roles)
                claimCollection.Add(new Claim(ClaimTypes.Role, role));

            claimCollection.Add(new Claim("tenantId", tenantId));
            claimCollection.Add(new Claim("accessId", accessId));
            claimCollection.Add(new Claim("name", name));
            claimCollection.Add(new Claim("email", email));
            claimCollection.Add(new Claim("changePassword", changePassword));

            // o que vai ter no token e configs
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // o que vai conter no token
                Subject = new ClaimsIdentity(claimCollection),

                // tempo de expiração
                Expires = DateTime.UtcNow.AddHours(10),

                // o tipo de criptografia do token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

            // gera o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using ServicesContracts;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServicesContracts.Models;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class TokenService : ITokenService
    {
        private readonly IHashPasswordService _hashPasswordService;

        public TokenService(IHashPasswordService hashPasswordService)
        {
            _hashPasswordService = hashPasswordService;
        }

        public async Task<string> GenerateToken(string username, string password)
        {
            var identity = await GetIdentity(username, password);

            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (encodedJwt);
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            User user = await _hashPasswordService.Logging(username, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            else
            {
                throw new Exception("Invalid login or password.");
            }
        }
    }
}

using Data.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication
{
    public class TokenProvider
    {
        public string CreateToken(LoginDto loginDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,loginDto.Email),
                new Claim(JwtRegisteredClaimNames.Iss,Environment.GetEnvironmentVariable("Issuer")),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now.AddMinutes(60).ToString()),
                new Claim(ClaimTypes.Role,loginDto.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Key")));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                Environment.GetEnvironmentVariable("Issuer"),
                Environment.GetEnvironmentVariable("Audience"),
                claims, DateTime.Now,
                DateTime.Now.AddMinutes(30),
                signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

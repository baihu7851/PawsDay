using ApplicationCore.Common;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PawsDayBackEnd.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _Configuration;
        private readonly IAppPasswordHasher _appPasswordHasher;
        public JwtHelper(IConfiguration configuration, IAppPasswordHasher appPasswordHasher)
        {
            _Configuration = configuration;
            _appPasswordHasher = appPasswordHasher;
        }

        public AuthResultDto GenerateToken(string username,int expireMinute=60)
        {
            var issuer = _Configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = _Configuration.GetValue<string>("JwtSettings:SignKey");
            var claims = CreateClaims(username);
            var userClaimsIdentity = new ClaimsIdentity(claims);
            // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
            // 用來產生數位簽章的密碼編譯演算法
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            // 預留位置，適用於和已發行權杖相關的所有屬性，用來定義JWT的相關設定
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Subject = userClaimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(expireMinute),
                SigningCredentials = signingCredentials
            };
            // 用來產生JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var response = new AuthResultDto();
            response.Token = tokenHandler.WriteToken(securityToken);
            response.ExpireTime = new DateTimeOffset(tokenDescriptor.Expires.Value).ToUnixTimeSeconds();

            return response;
        }

        public List<Claim> CreateClaims(string username)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Role, AuthorizationConstants.Administrator));
            return claims;
        }
    }
}

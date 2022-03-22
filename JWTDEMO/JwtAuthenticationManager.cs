using JWTDEMO.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTDEMO
{
    public class JwtAuthenticationManager
    {
        public JwtAuthResponse Authincate(string username,string password)
        {
            if(username != "admin" || password != "admin")
            {
                return null;
            }
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username",username),
                    new Claim(ClaimTypes.PrimaryGroupSid,"User Group 01")
                }),
                Expires = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new JwtAuthResponse
            {
                token = token,
                user_name = username,
                expires_in = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}

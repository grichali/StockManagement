using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key ; 
        private readonly IConfigurationSection _goolgeSettings;

        public TokenService(IConfiguration configuration)
        {
            _config = configuration ; 
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _goolgeSettings = configuration.GetSection("GoogleAuthSettings");
        }
        public string CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Id)
            };

            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tokenId){
            try{
                GoogleJsonWebSignature.ValidationSettings settigns = new GoogleJsonWebSignature.ValidationSettings(){
                    Audience = new List<string> {_goolgeSettings.GetSection("clientId").Value}
                };
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, settigns);
                return payload;
            }catch(Exception ex){
                return null;
            }
        }

    }
}
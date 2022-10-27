using System;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly string _secret;
        private readonly string _expDate;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly ITokenRepository _tokenRepository;
        public TokenService(string secret, string expDate, string issuer, string audience, ITokenRepository tokenRepository)
        {
            _secret = secret;
            _expDate = expDate;
            _issuer = issuer;
            _audience = audience;
            _tokenRepository = tokenRepository;
            // hvernig set ég tokenrepository herna innÐ?? ?

        }
        public JwtToken GenerateJwtToken(UserDto user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetSecurityTokenDescriptor(user);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenHandler.WriteToken(token);

            return _tokenRepository.CreateNewToken();

        }
        private SecurityTokenDescriptor GetSecurityTokenDescriptor(UserDto user)
        {
            var key = Encoding.ASCII.GetBytes(_secret);
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("name", user.Email),
                    new Claim("fullName", user.FullName),
                    new Claim("tokenId", user.TokenId.ToString())
                }),
                Audience = _audience,
                Issuer = _issuer,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Cryptocop.Software.API.Middleware
{
    public static class JwtAuthenticationMiddleware
    {
        public static AuthenticationBuilder AddJwtTokenAuthentication(this AuthenticationBuilder builder,
            IConfiguration config)
        {
            var jwtConfig = config.GetSection("JwtConfig");
            var secret = jwtConfig.GetSection("secret").Value;
            var issuer = jwtConfig.GetSection("issuer").Value;
            var audience = jwtConfig.GetSection("audience").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    NameClaimType = "name"
                };
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var claim = context.Principal.Claims.FirstOrDefault(c => c.Type == "tokenId")?.Value;
                        int.TryParse(claim, out var tokenId);
                        var tokenService = context.HttpContext.RequestServices.GetService<IJwtTokenService>();

                        if (tokenService.IsTokenBlacklisted(tokenId))
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("JWT token provided is invalid.");
                        }
                    }
                };
            });
            return builder;
        }
    }
}
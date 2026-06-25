using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.Infrastructure.ThirdPartyServices.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AdemideInfoWebsite.Application.Abstractions;

namespace AdemideInfoWebsite.Infrastructure.ThirdPartyServices;

public sealed class JwtTokenService(IOptions<JwtOptions> options) : IAuthTokenService
{
    public string CreateAccessToken(Profile user)
    {
        var jwt = options.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName), 
        };

        var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.UtcNow.AddMinutes(jwt.AccessTokenMinutes), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string CreateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}


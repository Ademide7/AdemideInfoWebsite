using AdemideInfoWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;

public interface IAuthTokenService
{
    string CreateAccessToken(Profile user); 
    string CreateRefreshToken(Profile user);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}

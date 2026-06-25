using AdemideInfoWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;

public interface IAuthTokenService
{
    string CreateAccessToken(Profile user);
    string CreateRefreshToken();
}

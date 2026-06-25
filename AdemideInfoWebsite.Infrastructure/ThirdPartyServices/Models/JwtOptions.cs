using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Infrastructure.ThirdPartyServices.Models;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = "FoodOrdering";
    public string Audience { get; set; } = "FoodOrderingClient";
    public string Secret { get; set; } = "Local-dev-secret-key-change-me-please-123456789";
    public int AccessTokenMinutes { get; set; } = 60;
}

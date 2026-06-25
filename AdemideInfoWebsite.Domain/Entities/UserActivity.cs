using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Domain.Entities;

public class UserActivity : Entity
{
    public string? UserId { get; private set; }
    public string? UserName { get; private set; }
    public UserActivityType ActivityType { get; private set; }
    public string IPAddress { get; private set; }
    public Guid ProfileId { get; private set; }
    public Profile Profile { get; private set; } = new Profile();
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Domain.Entities;

public class Password : Entity
{
    public string? HashedPassword { get; private set; } 
    public string? Salt { get; private set; }
    public DateTime? LastModifiedAt { get; private set; }

    public Guid ProfileID { get; private set; }
    public Profile Profile { get; private set; } = null!;

    // create password method
    public static Password CreatePassword(string hashedPassword, string salt)
    {
        return new Password
        {
            HashedPassword = hashedPassword,
            Salt = salt,
            LastModifiedAt = DateTime.UtcNow.AddHours(1)
        };
    }

    // update password method
    public void UpdatePassword(string hashedPassword, string salt)
    {
        if (!string.Equals(HashedPassword, hashedPassword, StringComparison.Ordinal))
        {
            HashedPassword = hashedPassword;
            Salt = salt;
            LastModifiedAt = DateTime.UtcNow.AddHours(1);
        }
    }

    public bool VerifyPassword(string hashedPassword, string salt)
    {
        return string.Equals(HashedPassword, hashedPassword, StringComparison.Ordinal) &&
               string.Equals(Salt, salt, StringComparison.Ordinal);
    }

}

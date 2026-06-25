using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Domain.Entities;

public class Profile : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public Language Language { get; private set; } = Language.English;
    public DateTime? LastModifiedAt { get; private set; }
    public Password? Password { get; private set; }
    public IReadOnlyList<Appointment> Appointments { get; private set; } = new List<Appointment>();
    public IReadOnlyList<UserActivity> UserActivities { get; private set; } = new List<UserActivity>();


    // create profile method
    public static Profile CreateProfile(string firstName, string lastName, string email, Language language)
    {
        return new Profile
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Language = language,
        };
    }

    // update Language method.
    public void UpdateLanguage(Language language)
    {
        Language = language;
        LastModifiedAt = DateTime.UtcNow.AddHours(1);
    }

    // update profile method
    public void UpdateProfile(string firstName, string lastName, string email)
    {
        if (!string.Equals(FirstName, firstName, StringComparison.Ordinal))
            FirstName = firstName;

        if (!string.Equals(LastName, lastName, StringComparison.Ordinal))
            LastName = lastName;

        if (!string.Equals(Email, email, StringComparison.OrdinalIgnoreCase))
            Email = email;
    }

    //Set password method
    public void SetPassword(Password password)
    {
        Password = password;
        LastModifiedAt = DateTime.UtcNow.AddHours(1);
    }
}

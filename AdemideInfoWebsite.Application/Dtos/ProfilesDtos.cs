using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Dtos;

// create profile record
public record CreateProfileDto(string FirstName, string LastName, string Email,string Password, Language Language);

// update profile record
public record UpdateProfileDto(string FirstName, string LastName, string Email);

// update profile language record
public record UpdateProfileLanguageDto(Language Language);

public record LoginProfileDto(string Email, string Password);

//EditProfileDto
public record EditProfileDto(string FirstName, string LastName, string Email);

//EditProfileLanguageDto
public record EditProfileLanguageDto(Language Language);

//ChangePasswordDto
public record ChangePasswordDto(string OldPassword, string NewPassword);

//SendPasswordResetEmailDto
public record SendPasswordResetEmailDto(string Email);

//RegisterResponseDto
public record RegistrationResponseDto(string token, ProfileDetailsDto ProfileDetails);

//ProfileDetailsDto
public record ProfileDetailsDto(Guid Id, string FirstName, string LastName, string Email, Language Language, DateTime CreatedAt, DateTime UpdatedAt);
//login response dto
public record LoginResponseDto(string token, ProfileDetailsDto ProfileDetails);

//edit profile response dto
public record EditProfileResponseDto(ProfileDetailsDto ProfileDetails);

//SendPasswordResetEmailResponseDto
public record SendPasswordResetEmailResponseDto(bool IsEmailSent, string Message);

public record ChangePasswordResponseDto(bool IsPasswordChanged, string Message);  

//profile to RegistrationResponseDto
public static class ProfileExtensions
{
    public static RegistrationResponseDto ToRegistrationResponseDto(this Domain.Entities.Profile profile, string token)
    {
        return new RegistrationResponseDto(token, new ProfileDetailsDto(profile.Id, profile.FirstName, profile.LastName, profile.Email, profile.Language, profile.CreatedAt, profile.LastModifiedAt.Value));
    }

    public static LoginResponseDto ToLoginResponseDto(this Domain.Entities.Profile profile, string token)
    {
        return new LoginResponseDto(token, new ProfileDetailsDto(profile.Id, profile.FirstName, profile.LastName, profile.Email, profile.Language, profile.CreatedAt, profile.LastModifiedAt.Value));
    }
}




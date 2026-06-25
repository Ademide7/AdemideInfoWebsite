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




using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Dtos;

// create profile record
public record CreateProfileDto(string FirstName, string LastName, string Email, Language Language);

// update profile record
public record UpdateProfileDto(string FirstName, string LastName, string Email);

// update profile language record
public record UpdateProfileLanguageDto(Language Language);




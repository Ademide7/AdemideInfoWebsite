using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;

public interface IProfileService
{
    Task<ResponseModel<Profile>> ChangePasswordAsync(Guid profileId, string oldPassword, string newPassword);
    Task<ResponseModel<Profile>> EditProfileAsync(Guid profileId, string firstName, string lastName, string email);
    Task<ResponseModel<Profile>> LoginAsync(string email, string password); 
    Task<ResponseModel<Profile>> RegisterAsync(string firstName, string lastName, string email, string password, Language language);
    Task<ResponseModel<Profile>> SendPasswordResetEmailAsync(string email);
    Task<ResponseModel<Profile>> UpdateProfileLanguageAsync(Guid profileId, Language language);
}

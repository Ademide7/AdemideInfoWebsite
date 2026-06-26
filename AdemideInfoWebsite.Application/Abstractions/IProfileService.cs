using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;

public interface IProfileService
{
    Task<ResponseModel> ChangePasswordAsync(Guid profileId, string oldPassword, string newPassword);
    Task<ResponseModel> EditProfileAsync(Guid profileId, string firstName, string lastName, string email);
    Task<ResponseModel<LoginResponseDto>> LoginAsync(string email, string password);
    Task<ResponseModel<LoginResponseDto>> RefreshTokenAsync(string token);
    Task<ResponseModel<RegistrationResponseDto>> RegisterAsync(string firstName, string lastName, string email, string password, Language language);
    Task<ResponseModel<SendPasswordResetEmailResponseDto>> SendPasswordResetEmailAsync(string email);
    Task<ResponseModel> UpdateProfileLanguageAsync(Guid profileId, Language language);
}

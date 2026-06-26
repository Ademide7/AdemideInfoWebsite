using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Application.Utilities;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.SharedKernel;
using AdemideInfoWebsite.SharedKernel;
using AdemideInfoWebsite.SharedKernel.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Util = AdemideInfoWebsite.SharedKernel.Utilities;

namespace AdemideInfoWebsite.Application.Services;

public class ProfileService(IUnitOfWork unitOfWork,IEmailService emailService, IOptions<AppSettings> appSettings,IAuthTokenService authTokenService) : IProfileService
{
    private readonly AppSettings _appSettings = appSettings.Value;
    private readonly IAuthTokenService _authTokenService = authTokenService;
 
     
    //login method to validate email and password, and return a response model with the profile data or an error message if not found.
    public async Task<ResponseModel<LoginResponseDto>> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return new ResponseModel<LoginResponseDto>(null, false, 400, "Email or password is null or empty", null);
        var profile = await unitOfWork.Repository<Domain.Entities.Profile>().FirstOrDefaultAsync(p => p.Email == email, includes: new Expression<Func<Profile, object>>[]
    { 
        x => x.Password
    });

        if (profile == null) return new ResponseModel<LoginResponseDto>(null, false, 404, "Profile not found", null);
        var hashedPassword = Util.HashPassword(password, email);

        if (profile.Password == null || !profile.Password.VerifyPassword(hashedPassword,email)) return new ResponseModel<LoginResponseDto>(null, false, 401, "Invalid password", null);


        string token = _authTokenService.CreateAccessToken(profile);
        var loginResponse = profile.ToLoginResponseDto(token);
         
        return new ResponseModel<LoginResponseDto>(loginResponse, true, 200, "Login successful", null);
    }

    // Register method to create a new profile with the given data, and return a response model with the profile data or an error message.
    public async Task<ResponseModel<RegistrationResponseDto>> RegisterAsync(string firstName, string lastName, string email, string password, Language language)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return new ResponseModel<RegistrationResponseDto>(null, false, 400, "First name, last name, email or password is null or empty", null);
        var existingProfile = await unitOfWork.Repository<Domain.Entities.Profile>().FirstOrDefaultAsync(p => p.Email == email);
        if (existingProfile != null) return new ResponseModel<RegistrationResponseDto>(null, false, 409, "Profile with the same email already exists", null);
        
        var profile = Domain.Entities.Profile.CreateProfile(firstName, lastName, email, language);
        var hashedPassword = Util.HashPassword(password,email);
        var passwordEntity = Password.CreatePassword(hashedPassword, email);
        profile.SetPassword(passwordEntity);

        var emailMessage = EmailTemplateAndBody.GenerateRegisterEmailTemplate(firstName, email, _appSettings.ContactEmail);
        await emailService.SendEmailAsync(new EmailServiceRequest(email, "Welcome to Ademide Info Website!", emailMessage));

        await unitOfWork.Repository<Domain.Entities.Profile>().AddAsync(profile);
        await unitOfWork.SaveChangesAsync();

        string token = _authTokenService.CreateAccessToken(profile);
        var registrationResponse = profile.ToRegistrationResponseDto(token);
        return new ResponseModel<RegistrationResponseDto>(registrationResponse, true, 201, "Profile created successfully", null);
    }

    //edit profile method to update the profile data with the given data, and return a response model with the profile data or an error message.
    public async Task<ResponseModel> EditProfileAsync(Guid profileId, string firstName, string lastName, string email)
    {
        var profile = await unitOfWork.Repository<Domain.Entities.Profile>().GetByIdAsync(profileId);
        if (profile == null) return new ResponseModel(false, 404, "Profile not found", null);
        profile.UpdateProfile(firstName, lastName, email);
        unitOfWork.Repository<Domain.Entities.Profile>().Update(profile);
        await unitOfWork.SaveChangesAsync();

        var emailMessage = EmailTemplateAndBody.GenerateUpdateProfileEmailTemplate(firstName,_appSettings.ContactEmail);
        await emailService.SendEmailAsync(new EmailServiceRequest(email, "Profile Updated", emailMessage));

        return new ResponseModel(true, 200, "Profile updated successfully", null);
    }

    public async Task<ResponseModel> UpdateProfileLanguageAsync(Guid profileId, Language language)
    {
        var profile = await unitOfWork.Repository<Domain.Entities.Profile>().GetByIdAsync(profileId);
        if (profile == null) return new ResponseModel(false, 404, "Profile not found", null);
        profile.UpdateLanguage(language);
        unitOfWork.Repository<Domain.Entities.Profile>().Update(profile);
        await unitOfWork.SaveChangesAsync();
        return new ResponseModel(true, 200, "Profile language updated successfully", null);
    }

    //change password method to update the profile password with the given data, and return a response model with the profile data or an error message.
    public async Task<ResponseModel> ChangePasswordAsync(Guid profileId, string oldPassword, string newPassword)
    {
        var profile = await unitOfWork.Repository<Domain.Entities.Profile>().GetByIdAsync(profileId);
        if (profile == null) return new ResponseModel(false, 404, "Profile not found", null);
        var hashedOldPassword = Util.HashPassword(oldPassword, profile.Email);
        if (profile.Password == null || !profile.Password.VerifyPassword(hashedOldPassword, profile.Email)) return new ResponseModel(false, 401, "Invalid old password", null);
        var hashedNewPassword = Util.HashPassword(newPassword, profile.Email);
        var passwordEntity = Password.CreatePassword(hashedNewPassword, profile.Email);
        profile.SetPassword(passwordEntity);
        unitOfWork.Repository<Domain.Entities.Profile>().Update(profile);
        await unitOfWork.SaveChangesAsync();

        var emailMessage = EmailTemplateAndBody.GeneratePasswordChangeConfirmationEmailTemplate(profile.FirstName, _appSettings.ContactEmail);
        await emailService.SendEmailAsync(new EmailServiceRequest(profile.Email, "Password Changed", emailMessage));

        return new ResponseModel(true, 200, "Profile password changed successfully", null);
    }

    //Send password reset email method to send a password reset email to the profile email, and return a response model with the profile data or an error message.
    public async Task<ResponseModel<SendPasswordResetEmailResponseDto>> SendPasswordResetEmailAsync(string email)
    {
        var profile = await unitOfWork.Repository<Domain.Entities.Profile>().FirstOrDefaultAsync(p => p.Email == email);
        if (profile == null) return new ResponseModel<SendPasswordResetEmailResponseDto>(null, false, 404, "Profile not found", null);
        var resetToken = Util.GenerateRandomPassword(8);
        var hashedResetToken = Util.HashPassword(resetToken, profile.Email);
        var passwordEntity = Password.CreatePassword(hashedResetToken, profile.Email);
        profile.SetPassword(passwordEntity);
        unitOfWork.Repository<Domain.Entities.Profile>().Update(profile);
        await unitOfWork.SaveChangesAsync();

        var emailMessage = EmailTemplateAndBody.GeneratePasswordResetEmailTemplate(profile.FirstName, resetToken, _appSettings.ContactEmail);
        await emailService.SendEmailAsync(new EmailServiceRequest(profile.Email, "Password Reset Request", emailMessage));

        var responseDto = new SendPasswordResetEmailResponseDto(true, "Password reset email sent successfully");
        return new ResponseModel<SendPasswordResetEmailResponseDto>(responseDto, true, 200, "Password reset email sent successfully", null);
    }

    //refresh token  method.
    public async Task<ResponseModel<LoginResponseDto>> RefreshTokenAsync(string token)
    {
        var principal = _authTokenService.GetPrincipalFromExpiredToken(token);
        if (principal == null) return new ResponseModel<LoginResponseDto>(null, false, 401, "Invalid token", null);
        var email = principal.Identity?.Name;
        if (string.IsNullOrWhiteSpace(email)) return new ResponseModel<LoginResponseDto>(null, false, 401, "Invalid token", null);
        var profile = await unitOfWork.Repository<Domain.Entities.Profile>().FirstOrDefaultAsync(p => p.Email == email);
        if (profile == null) return new ResponseModel<LoginResponseDto>(null, false, 404, "Profile not found", null);
        string newToken = _authTokenService.CreateAccessToken(profile);
        var loginResponse = profile.ToLoginResponseDto(newToken);
        return new ResponseModel<LoginResponseDto>(loginResponse, true, 200, "Token refreshed successfully", null);
    }

}

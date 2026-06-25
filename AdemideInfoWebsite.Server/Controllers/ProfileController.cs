using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.SharedKernel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdemideInfoWebsite.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IProfileService profileService) : ControllerBase
{
    // endpoints for all profile related operations
    [HttpPost("create")]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto createProfileDto)
    {
        var result = await profileService.RegisterAsync(createProfileDto.FirstName, createProfileDto.LastName, createProfileDto.Email, createProfileDto.Password, createProfileDto.Language);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginProfileDto loginDto)
    {
        var result = await profileService.LoginAsync(loginDto.Email, loginDto.Password);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("edit")]   
    public async Task<IActionResult> EditProfile([FromBody] EditProfileDto editProfileDto)
    {
        var userIdClaim = ClaimsExtensions.CurrentUserId(User);
        if (userIdClaim == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
              new ResponseModel<bool>(false, false, StatusCodes.Status401Unauthorized, "User is not authenticated.", null));
        }
        var result = await profileService.EditProfileAsync(userIdClaim, editProfileDto.FirstName, editProfileDto.LastName, editProfileDto.Email);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userIdClaim = ClaimsExtensions.CurrentUserId(User);
        if (userIdClaim == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
              new ResponseModel<bool>(false, false, StatusCodes.Status401Unauthorized, "User is not authenticated.", null));
        }
        var result = await profileService.ChangePasswordAsync(userIdClaim, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("send-password-reset-email")]
    public async Task<IActionResult> SendPasswordResetEmail([FromBody] SendPasswordResetEmailDto sendPasswordResetEmailDto)
    {
        var result = await profileService.SendPasswordResetEmailAsync(sendPasswordResetEmailDto.Email);
        return StatusCode(result.StatusCode, result);
    }

}

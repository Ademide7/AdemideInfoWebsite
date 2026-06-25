using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.SharedKernel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdemideInfoWebsite.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    // endpoints for all appointment related operations
    [HttpPost("create")]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        var userClaimId = ClaimsExtensions.CurrentUserId(User);
        if(userClaimId == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
              new ResponseModel<bool>(false, false, StatusCodes.Status401Unauthorized, "User is not authenticated.", null));
        }
        var result = await appointmentService.CreateAppointmentAsync(userClaimId, createAppointmentDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("get-appointments")]
    public async Task<IActionResult> GetAppointments([FromBody] GetAppointmentsDto getAppointmentsDto)
    {
        var userClaimId = ClaimsExtensions.CurrentUserId(User);
        if (userClaimId == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
              new ResponseModel<bool>(false, false, StatusCodes.Status401Unauthorized, "User is not authenticated.", null));
        }
        var result = await appointmentService.GetAppointmentsByProfileIdAsync(userClaimId, getAppointmentsDto.PageNumber, getAppointmentsDto.PageSize, getAppointmentsDto.SortBy, getAppointmentsDto.IsAscending);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("mark-as-completed")]
    public async Task<IActionResult> MarkAppointmentAsCompleted()
    {
        var userClaimId = ClaimsExtensions.CurrentUserId(User);
        if (userClaimId == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
              new ResponseModel<bool>(false, false, StatusCodes.Status401Unauthorized, "User is not authenticated.", null));
        }
        var result = await appointmentService.MarkAppointmentAsCompletedAsync(userClaimId);
        return StatusCode(result.StatusCode, result);
    }
     
}

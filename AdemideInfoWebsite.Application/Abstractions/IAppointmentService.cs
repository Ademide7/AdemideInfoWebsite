using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;

public interface IAppointmentService
{
    Task<ResponseModel<bool>> CreateAppointmentAsync(Guid profileId,CreateAppointmentDto createAppointmentDto);
    Task<ResponseModel<List<Appointment>>> GetAppointmentsByProfileIdAsync(Guid profileId, int pageNumber, int pageSize, string sortBy, bool isAscending);
    Task<ResponseModel<bool>> MarkAppointmentAsCompletedAsync(Guid appointmentId);
}

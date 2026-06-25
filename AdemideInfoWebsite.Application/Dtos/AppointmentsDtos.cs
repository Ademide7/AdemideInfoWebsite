using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Dtos;

//create appointment record
public record CreateAppointmentDto(string Title, string Description, DateTime AppointmentDate);

//update appointment record
public record UpdateAppointmentDto(string Title, string Description, DateTime AppointmentDate);

//mark appointment as completed record
public record MarkAppointmentAsCompletedDto(bool IsCompleted);

//GetAppointmentsDto
public record GetAppointmentsDto(int PageNumber, int PageSize, string SortBy, bool IsAscending);

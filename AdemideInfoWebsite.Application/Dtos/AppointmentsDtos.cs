using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Dtos;

//create appointment record
public record CreateAppointmentDto(string Title, string Description, DateTime AppointmentDate, Guid ProfileId);

//update appointment record
public record UpdateAppointmentDto(string Title, string Description, DateTime AppointmentDate);

//mark appointment as completed record
public record MarkAppointmentAsCompletedDto(bool IsCompleted);

using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Application.Utilities;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.SharedKernel.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Services;

public class AppointmentService(IUnitOfWork unitOfWork,IEmailService emailService,IOptions<AppSettings> appSettings) : IAppointmentService
{
   private AppSettings _appSettings = appSettings.Value;

    // Method to create an appointment and send a confirmation email using dtos.
    // check if there is an existing appointment that is still active and not completed, mark it as completed and create a new appointment. 

    public async Task<ResponseModel<bool>> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
    {
        //validate profileId.
        var profile = await unitOfWork.Repository<Profile>().GetByIdAsync(createAppointmentDto.ProfileId);
        if (profile is null) return new ResponseModel<bool>(false, false, 400, "Something went wrong!", null);

        var existingAppointment = await unitOfWork.Repository<Appointment>().FirstOrDefaultAsync(a => a.ProfileId == createAppointmentDto.ProfileId && !a.IsCompleted);
        if (existingAppointment != null)
        {
            existingAppointment.MarkAsCompleted();
            unitOfWork.Repository<Appointment>().Update(existingAppointment);
            await unitOfWork.SaveChangesAsync();
        }

        var appointment = Appointment.CreateAppointment(createAppointmentDto.Title, createAppointmentDto.Description, createAppointmentDto.AppointmentDate);
        await unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await unitOfWork.SaveChangesAsync();
        // Send confirmation email

        var emaailMessage = EmailTemplateAndBody.GenerateAppointmentConfirmationEmailTemplate(profile.FirstName, createAppointmentDto.AppointmentDate.ToString("f"), createAppointmentDto.AppointmentDate.ToString("t"), _appSettings.ContactEmail);
        await emailService.SendEmailAsync(new EmailServiceRequest(profile.Email, "Appointment Confirmation",emaailMessage));

        return new ResponseModel<bool>(
        true,
        true,
        200,
        "Appointment created and confirmation email sent."
    ); ;
    }

    // Method to get all appointments for a specific profile with pagination and sorting.
    public async Task<ResponseModel<List<Appointment>>> GetAppointmentsByProfileIdAsync(Guid profileId, int pageNumber, int pageSize, string sortBy, bool isAscending)
    {
        var profile = await unitOfWork.Repository<Profile>().GetByIdAsync(profileId);
        if (profile is null) return new ResponseModel<List<Appointment>>(null!, false, 400, "Something went wrong!", null);
        Func<IQueryable<Appointment>, IQueryable<Appointment>> shape = query =>
        {
            query = query.Where(a => a.ProfileId == profileId);
            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "title" => isAscending ? query.OrderBy(a => a.Title) : query.OrderByDescending(a => a.Title),
                "appointmentdate" => isAscending ? query.OrderBy(a => a.AppointmentDate) : query.OrderByDescending(a => a.AppointmentDate),
                _ => query.OrderBy(a => a.AppointmentDate), // Default sorting by AppointmentDate
            };
            // Apply pagination
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        };
        var appointments = await unitOfWork.Repository<Appointment>().ListAsync(shape);
        var paginationModel = new PaginationModel
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = await unitOfWork.Repository<Appointment>().CountAsync(a => a.ProfileId == profileId),
            TotalPages = (int)Math.Ceiling((double)await unitOfWork.Repository<Appointment>().CountAsync(a => a.ProfileId == profileId) / pageSize)
        };
        return new ResponseModel<List<Appointment>>(appointments, true, 200, "Appointments retrieved successfully.", paginationModel);
    }

    public async Task<ResponseModel<bool>> MarkAppointmentAsCompletedAsync(Guid appointmentId)
    {
        var appointment = await unitOfWork.Repository<Appointment>().GetByIdAsync(appointmentId);
        if (appointment is null) return new ResponseModel<bool>(false, false, 400, "Something went wrong!", null);
        appointment.MarkAsCompleted();
        unitOfWork.Repository<Appointment>().Update(appointment);
        await unitOfWork.SaveChangesAsync();
        return new ResponseModel<bool>(true, true, 200, "Appointment marked as completed.", null);
    }
}

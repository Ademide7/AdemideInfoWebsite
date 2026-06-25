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

        var existingAppointment = await unitOfWork.Repository<Appointment>().FirstOrDefaultAsync(a => a. == createAppointmentDto.ProfileId && !a.IsCompleted);
        if (existingAppointment != null)
        {
            existingAppointment.IsCompleted = true;
            await unitOfWork.Repository<Appointment>().Update(existingAppointment);
        }

        var appointment = Appointment.CreateAppointment(createAppointmentDto.Title, createAppointmentDto.Description, createAppointmentDto.AppointmentDate);
        await unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await unitOfWork.SaveChangesAsync();
        // Send confirmation email

        var emaailMessage = EmailTemplateAndBody.GenerateAppointmentConfirmationEmailTemplate(profile.FirstName, createAppointmentDto.AppointmentDate.ToString("f"), createAppointmentDto.AppointmentDate.ToString("t"), _appSettings.ContactEmail);
        await emailService.SendEmailAsync(new EmailServiceRequest(profile.Email, "Welcome to Ademide Info Website!", emailMessage));

        return new ResponseModel<bool>(
        true,
        true,
        200,
        "Appointment created and confirmation email sent."
    ); ;
    }


}

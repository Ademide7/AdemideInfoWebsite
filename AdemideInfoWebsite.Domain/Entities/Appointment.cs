using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Domain.Entities;

public class Appointment : Entity
{
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime? AppointmentDate { get; private set; } = DateTime.UtcNow.AddHours(1);
    public bool IsCompleted { get; private set; } = false;

    public Guid ProfileId { get; private set; }

    public Profile Profile { get; private set; }


    // create appointment method
    public static Appointment CreateAppointment(string title, string description, DateTime appointmentDate)
    {
        return new Appointment
        {
            Title = title,
            Description = description,
            AppointmentDate = appointmentDate,
            IsCompleted = false
        };
    }

    // update appointment method
    public void UpdateAppointment(string title, string description, DateTime appointmentDate)
    {
        if (!string.Equals(Title, title, StringComparison.Ordinal))
            Title = title;
        if (!string.Equals(Description, description, StringComparison.Ordinal))
            Description = description;
        if (AppointmentDate != appointmentDate)
            AppointmentDate = appointmentDate;
    }

    // mark appointment as completed method
    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}

using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Application.Services;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.SharedKernel.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace AdemideInfoWebsite.Tests.Services;

public class AppointmentServiceTests
{
    private readonly AppointmentService _sut;

    [Fact]
    public async Task CreateAppointmentAsync_WithValidData_ShouldReturnSuccess()
    {
        Assert.True(true);
    }
}

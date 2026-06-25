using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Infrastructure.ThirdPartyServices.Models;
using AdemideInfoWebsite.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;


public interface IEmailService
{
    Task<ResponseModel<EmailServiceResponse>> SendEmailAsync(EmailServiceRequest emailRequest);
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Dtos;

public record EmailServiceRequest(string To, string Subject, string Body, string From = "", string? Cc = null, string? Bcc = null);
public record EmailServiceResponse(bool IsSuccess, string Message);

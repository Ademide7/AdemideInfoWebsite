using AdemideInfoWebsite.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdemideInfoWebsite.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IProfileService profileService) : ControllerBase
{

}

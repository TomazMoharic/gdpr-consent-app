using GdprConsentApp.Interfaces;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GdprConsentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersConsentsController
{
    private readonly IUsersConsentsService _usersConsentsService;

    public UsersConsentsController(IUsersConsentsService usersConsentsService)
    {
        _usersConsentsService = usersConsentsService;
    }

    [HttpPost]
    public ActionResult<EntityCreatedResponseModel> AddUserConsent([FromBody] AddUserConsentRequestModel model)
    {
        return _usersConsentsService.AddUserConsent(model);
    }
    
    [HttpDelete("{idUserConsent}")]
    public ActionResult<EntityDeletedResponseModel> RemoveUserConsent ([FromRoute] int idUserConsent)
    {
        return _usersConsentsService.RemoveUserConsent(idUserConsent);
    }
    
    /// <summary>
    /// Get all users that have given the request consent: <paramref name="idConsent"/>.
    /// </summary>
    /// <param name="idConsent"></param>
    /// <returns></returns>
    [HttpGet("{idConsent}")]
    public ActionResult<UsersWithConsentModel> GetUsersWithConsent([FromRoute] int idConsent)
    {
        return _usersConsentsService.GetUsersWithConsent(idConsent);
    }
}
using GdprConsentApp.Interfaces;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GdprConsentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost]
    public ActionResult<EntityCreatedResponseModel> Create([FromBody] CreateUserRequestModel model)
    {
        return _usersService.Create(model);
    }
    
    [HttpGet]
    public ActionResult<List<UserSimpleModel>> GetUsers([FromQuery] GetUsersRequestModel model)
    {
        return _usersService.GetUsers(model);
    }

    [HttpGet("{idUser}")]
    public ActionResult<UserModel> GetById([FromRoute] int idUser)
    {
        return _usersService.GetById(idUser);
    }
    
    [HttpPatch("{idUser}")]
    public ActionResult<EntityUpdatedResponseModel> Update([FromRoute] int idUser, [FromBody] UpdateUserRequestModel model)
    {
        return _usersService.Update(idUser, model);
    }
    
    [HttpDelete("{idUser}")]
    public ActionResult<EntityDeletedResponseModel> Delete([FromRoute] int idUser)
    {
        return _usersService.Delete(idUser);
    }
}
using GdprConsentApp.Interfaces;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GdprConsentApp.Controllers;

[ApiController]
[Route("[controller]")]

public class ConsentsController
{
    private readonly IConsentsService _consentsService;

    public ConsentsController(IConsentsService consentsService)
    {
        _consentsService = consentsService;
    }

    [HttpPost]
    public ActionResult<EntityCreatedResponseModel> Create([FromBody] CreateConsentRequestModel model)
    {
        return _consentsService.Create(model);
    }
    
    [HttpGet]
    public ActionResult<List<ConsentModel>> GetConsents([FromQuery] GetConsentsRequestModel model)
    {
        return _consentsService.GetConsents(model);
    }

    [HttpGet("{idConsent}")]
    public ActionResult<ConsentModel> GetById([FromRoute] int idConsent)
    {
        return _consentsService.GetById(idConsent);
    }
    
    [HttpPatch("{idConsent}")]
    public ActionResult<EntityUpdatedResponseModel> Update([FromRoute] int idConsent, [FromBody] UpdateConsentRequestModel model)
    {
        return _consentsService.Update(idConsent, model);
    }
    
    [HttpDelete("{idConsent}")]
    public ActionResult<EntityDeletedResponseModel> Delete([FromRoute] int idConsent)
    {
        return _consentsService.Delete(idConsent);
    }
}
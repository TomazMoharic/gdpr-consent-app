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

    /// <summary>
    /// Creat a new consent. 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<EntityCreatedResponseModel> Create([FromBody] CreateConsentRequestModel model)
    {
        return _consentsService.Create(model);
    }
    
    /// <summary>
    /// Get all consents filtered by request model properties.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<ConsentModel>> GetConsents([FromQuery] GetConsentsRequestModel model)
    
    {
        return _consentsService.GetConsents(model);
    }

    /// <summary>
    /// Get a consent for the given <paramref name="idConsent"/>
    /// </summary>
    /// <param name="idConsent"></param>
    /// <returns></returns>
    [HttpGet("{idConsent}")]
    public ActionResult<ConsentModel> GetById([FromRoute] int idConsent)
    {
        return _consentsService.GetById(idConsent);
    }
    
    /// <summary>
    /// Update a consent for the given <paramref name="idConsent"/>
    /// </summary>
    /// <param name="idConsent"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPatch("{idConsent}")]
    public ActionResult<EntityUpdatedResponseModel> Update([FromRoute] int idConsent, [FromBody] UpdateConsentRequestModel model)
    {
        return _consentsService.Update(idConsent, model);
    }
    
    /// <summary>
    /// Delete a consent for the given <paramref name="idConsent"/>
    /// </summary>
    /// <param name="idConsent"></param>
    /// <returns></returns>
    [HttpDelete("{idConsent}")]
    public ActionResult<EntityDeletedResponseModel> Delete([FromRoute] int idConsent)
    {
        return _consentsService.Delete(idConsent);
    }
}
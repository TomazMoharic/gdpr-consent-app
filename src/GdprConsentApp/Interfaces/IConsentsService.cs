using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;

namespace GdprConsentApp.Interfaces;

public interface IConsentsService
{
    public EntityCreatedResponseModel Create(CreateConsentRequestModel model);
    public List<ConsentModel> GetConsents(GetConsentsRequestModel model);
    public ConsentModel GetById(int idConsent);
    public EntityUpdatedResponseModel Update (int idConsent, UpdateConsentRequestModel model);
    public EntityDeletedResponseModel Delete (int idConsent);
}
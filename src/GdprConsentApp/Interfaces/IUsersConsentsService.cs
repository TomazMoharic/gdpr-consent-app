using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;

namespace GdprConsentApp.Interfaces;

public interface IUsersConsentsService
{
    EntityCreatedResponseModel AddUserConsent(AddUserConsentRequestModel model);
    EntityDeletedResponseModel RemoveUserConsent(int idUserConsent);
    UsersWithConsentModel GetUsersWithConsent(int idConsent);
}
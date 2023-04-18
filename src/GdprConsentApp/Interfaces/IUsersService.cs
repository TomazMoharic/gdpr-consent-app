using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;

namespace GdprConsentApp.Interfaces;

public interface IUsersService
{
    List<UserSimpleModel> GetUsers(GetUsersRequestModel model);
    UserModel GetById(int idUser);
    EntityCreatedResponseModel Create(CreateUserRequestModel model);
    EntityUpdatedResponseModel Update(int idUser, UpdateUserRequestModel model);
    EntityDeletedResponseModel Delete(int idUser); 
}
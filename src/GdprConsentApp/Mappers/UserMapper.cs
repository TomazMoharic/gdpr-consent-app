using GdprConsentApp.Database;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;

namespace GdprConsentApp.Mappers;

public static class UserMapper
{
    public static TUsers ToUserDbModel(this CreateUserRequestModel userModel)
    {
        TUsers userDbModel = new()
        {
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
        };
        
        return userDbModel;
    }
}

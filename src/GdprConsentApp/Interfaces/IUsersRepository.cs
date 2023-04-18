using GdprConsentApp.Database;
using GdprConsentApp.Models;

namespace GdprConsentApp.Repository.Interfaces;

public interface IUsersRepository
{
    List<UserSimpleModel> GetUsersFromDb(string? inputQuery);
    UserModel? GetUserWithConsentsFromDb(int idUser);
    bool CheckIfUserWithThisEmailExistsInDb(string email);
    int InsertUserToDb(TUsers user);
    TUsers? GetUserFromDb(int idUser);
    void DeleteUserFromDb(TUsers user);
    void UpdateUserInDb(TUsers user);
}
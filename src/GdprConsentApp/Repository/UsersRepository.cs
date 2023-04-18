using GdprConsentApp.Database;
using GdprConsentApp.Helpers;
using GdprConsentApp.Models;
using GdprConsentApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GdprConsentApp.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly MysqlDbContext _db;

    public UsersRepository(MysqlDbContext db)
    {
        _db = db;
    }

    public List<UserSimpleModel> GetUsersFromDb(string? inputQuery)
    {
        List<UserSimpleModel> users = _db.Users
            .ConditionalQueryable(!String.IsNullOrWhiteSpace(inputQuery), query => query
                .WhereUsersByInputQuery(inputQuery))
            .Select(u => new UserSimpleModel
            {
                IdUser = u.IdUser,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
            })
            .ToList();

        return users;
    }

    public UserModel? GetUserWithConsentsFromDb(int idUser)
    {
        UserModel? user = _db.Users
            .Include(u => u.Consents)
            .ThenInclude(c => c.Consent)
            .Where(u => u.IdUser == idUser )
            .Select(u => new UserModel
            {
                IdUser = u.IdUser,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                Consents = u.Consents != null
                    ? u.Consents
                        .Select(c => new ConsentSimpleModel
                        {
                            IdConsent = c.IdConsent,
                            Name = c.Consent.Name,
                            Code = c.Consent.Code,
                            ConsentDurationDays = c.Consent.ConsentDurationDays,
                        })
                        .ToList()
                    : new List<ConsentSimpleModel>(),
            })
            .FirstOrDefault();

        return user;
    }

    public bool CheckIfUserWithThisEmailExistsInDb(string email)
    {
        bool userWithThisEmailAlreadyExists = _db.Users.Any(u => u.Email == email);

        return userWithThisEmailAlreadyExists;
    }

    public int InsertUserToDb(TUsers user)
    {
        var createdUser =_db.Users.Add(user);

        _db.SaveChanges();

        return createdUser.Entity.IdUser;
    }

    public TUsers? GetUserFromDb(int idUser)
    {
        TUsers? user = _db.Users.FirstOrDefault(u => u.IdUser == idUser);

        return user;
    }

    public void DeleteUserFromDb(TUsers user)
    {
        _db.Users.Remove(user);

        _db.SaveChanges();
    }

    public void UpdateUserInDb(TUsers user)
    {
        _db.Users.Update(user);

        _db.SaveChanges();
    }
}
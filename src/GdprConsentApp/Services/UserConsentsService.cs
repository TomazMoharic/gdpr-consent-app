using GdprConsentApp.Database;
using GdprConsentApp.Exceptions;
using GdprConsentApp.Interfaces;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace GdprConsentApp.Services;

public class UserConsentsService : IUsersConsentsService
{
    private readonly MysqlDbContext _db;

    public UserConsentsService(MysqlDbContext db)
    {
        _db = db;
    }
    
    public EntityCreatedResponseModel AddUserConsent(AddUserConsentRequestModel model)
    {
        TConsents? consent = _db.Consents.FirstOrDefault(u => u.IdConsent == model.IdConsent);

        if (consent is null)
        {
            throw new EntityNotFoundException("Consent with this id does not exist.");
        }
        
        TUsers? user = _db.Users.FirstOrDefault(u => u.IdUser == model.IdUser);
        
        if (user is null)
        {
            throw new EntityNotFoundException("User with this id does not exist.");
        }

        TUsersConsents userConsentToAdd = new()
        {
            IdConsent = model.IdConsent,
            IdUser = model.IdUser,
        };

        _db.UsersConsents.Add(userConsentToAdd);

        _db.SaveChanges();

        return new EntityCreatedResponseModel(userConsentToAdd.IdUserConsent, "Successfully added user's consent.");
    }

    public EntityDeletedResponseModel RemoveUserConsent(int idUserConsent)
    {
        TUsersConsents? userConsent = _db.UsersConsents.FirstOrDefault(uc => uc.IdUserConsent == idUserConsent);

        if (userConsent is null)
        {
            throw new EntityNotFoundException("User consent with this id does not exist.");
        }

        _db.UsersConsents.Remove(userConsent);

        _db.SaveChanges();

        return new EntityDeletedResponseModel("User consent successfully removed.");
    }

    public UsersWithConsentModel GetUsersWithConsent(int idConsent)
    {
        TConsents? consent = _db.Consents.FirstOrDefault(u => u.IdConsent == idConsent);

        if (consent is null)
        {
            throw new EntityNotFoundException("Consent with this id does not exist.");
        }

        UsersWithConsentModel usersWithConsent = _db.Consents
            .Include(c => c.Users)
                .ThenInclude(uc => uc.User)
            .Where(u => u.IdConsent == idConsent)
            .Select(u => new UsersWithConsentModel
            {
                Consent = new ConsentSimpleModel
                {
                    IdConsent = u.IdConsent,
                    Name = u.Name,
                    Code = u.Code,
                    ConsentDurationDays = u.ConsentDurationDays,
                },
                Users = u.Users != null 
                    ? u.Users
                        .Select(uc => new UserSimpleModel
                        {
                            IdUser = uc.User.IdUser,
                            FirstName = uc.User.FirstName,
                            LastName = uc.User.LastName,
                            Email = uc.User.Email,
                        })
                        .ToList()
                    : new List<UserSimpleModel>(),
            })
            .First(); // it was already checked that a consent with the given id exists

        return usersWithConsent;
    }
}
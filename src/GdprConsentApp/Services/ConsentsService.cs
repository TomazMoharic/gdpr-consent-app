using GdprConsentApp.Database;
using GdprConsentApp.Exceptions;
using GdprConsentApp.Interfaces;
using GdprConsentApp.Models;
using GdprConsentApp.RequestModels;
using GdprConsentApp.Helpers;

namespace GdprConsentApp.Services;

public class ConsentsService : IConsentsService
{
    private readonly MysqlDbContext _db;

    public ConsentsService(MysqlDbContext db)
    {
        _db = db;
    }
    public EntityCreatedResponseModel Create(CreateConsentRequestModel model)
    {
        if (model.ConsentDurationDays <= 0)
        {
            throw new BadRequestException("Consent duration bust be larger then zero.");
        }

        TConsents consentToInsert = new()
        {
            Name = model.Name,
            Code = model.Code,
            ConsentDurationDays = model.ConsentDurationDays,
        };

        _db.Consents.Add(consentToInsert);

        _db.SaveChanges();

        return new EntityCreatedResponseModel(consentToInsert.IdConsent, "Consent successfully created.");
    }

    public List<ConsentModel> GetConsents(GetConsentsRequestModel model)
    {
        List<ConsentModel> consents = _db.Consents
            .ConditionalQueryable(!String.IsNullOrWhiteSpace(model.Query), query => query
                .Where(c => c.Name.Contains(model.Query)))
            .Select(c => new ConsentModel
            {
                IdConsent = c.IdConsent,
                Name = c.Name,
                Code = c.Code,
                ConsentDurationDays = c.ConsentDurationDays,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            })
            .ToList();

        return consents;
    }

    public ConsentModel GetById(int idConsent)
    {
        ConsentModel? consent = _db.Consents
            .Select(c => new ConsentModel
            {
                IdConsent = c.IdConsent,
                Name = c.Name,
                Code = c.Code,
                ConsentDurationDays = c.ConsentDurationDays,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            })
            .FirstOrDefault(c => c.IdConsent == idConsent);

        if (consent is null)
        {
            throw new EntityNotFoundException("Consent with this id could not be found.");
        }

        return consent;
    }

    public EntityUpdatedResponseModel Update(int idConsent, UpdateConsentRequestModel model)
    {
        TConsents? consent = _db.Consents.FirstOrDefault(c => c.IdConsent == idConsent);

        if (consent is null)
        {
            throw new EntityNotFoundException("Consent with this id could not be found.");
        }
        
        if (model.ConsentDurationDays is not null && model.ConsentDurationDays <= 0)
        {
            throw new BadRequestException("Consent duration bust be larger then zero.");
        }

        consent.Name = model.Name ?? consent.Name;
        consent.Code = model.Code ?? consent.Code;
        consent.ConsentDurationDays = model.ConsentDurationDays ?? consent.ConsentDurationDays;

        _db.SaveChanges();

        return new EntityUpdatedResponseModel("Consent successfully updated.");
    }

    public EntityDeletedResponseModel Delete(int idConsent)
    {
        TConsents? consent = _db.Consents.FirstOrDefault(c => c.IdConsent == idConsent);

        if (consent is null)
        {
            throw new EntityNotFoundException("Consent with this id could not be found.");
        }

        _db.Consents.Remove(consent);

        _db.SaveChanges();

        return new EntityDeletedResponseModel("Consent successfully deleted.");
    }
}
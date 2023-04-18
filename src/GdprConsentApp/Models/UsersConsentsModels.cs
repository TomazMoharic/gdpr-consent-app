namespace GdprConsentApp.Models;

public class UsersWithConsentModel
{
    public ConsentSimpleModel Consent { get; set; }
    public List<UserSimpleModel> Users { get; set; }
}
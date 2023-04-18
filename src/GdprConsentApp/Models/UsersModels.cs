namespace GdprConsentApp.Models;

public class UserModel
{
    public int IdUser { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<ConsentSimpleModel> Consents { get; set; }
}

public class UserSimpleModel
{
    public int IdUser { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
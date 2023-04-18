using System.ComponentModel.DataAnnotations;

namespace GdprConsentApp.RequestModels;

public class GetUsersRequestModel
{
    public string? Query { get; set; }
}

public class CreateUserRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}

public class UpdateUserRequestModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
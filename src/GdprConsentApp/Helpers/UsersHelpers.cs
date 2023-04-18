using GdprConsentApp.Database;

namespace GdprConsentApp.Helpers;

public static class UsersHelpers
{
    public static IQueryable<TUsers> WhereUsersByInputQuery(this IQueryable<TUsers> users, string inputQuery)
    {
        string inputQueryWithoutWhiteSpaces = inputQuery.RemoveWhitespace();

        return users.Where(u =>
            (u.FirstName + u.LastName).Contains(inputQueryWithoutWhiteSpaces) ||
            (u.LastName + u.FirstName).Contains(inputQueryWithoutWhiteSpaces) ||
            u.Email.Contains(inputQueryWithoutWhiteSpaces));
    }
    
    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }
}
namespace GdprConsentApp.Helpers;

public static class GeneralHelpers
{
    
    /// <summary>
    /// When <paramref name="condition"/> is true modify the <paramref name="query"/> by invoking <paramref name="modifyQueryable"/> function on it.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    /// <param name="modifyQueryable"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> ConditionalQueryable<T>(this IQueryable<T> query, bool condition, Func<IQueryable<T>, IQueryable<T>> modifyQueryable)
    {
        if (condition)
        {
            query = modifyQueryable(query);
        }

        return query;
    }
}
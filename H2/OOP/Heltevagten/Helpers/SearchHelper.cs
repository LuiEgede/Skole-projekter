namespace Heltevagten.Helpers;

public static class SearchHelper
{
    public static T? FindFirst<T>(List<T> items, Func<T, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }
}
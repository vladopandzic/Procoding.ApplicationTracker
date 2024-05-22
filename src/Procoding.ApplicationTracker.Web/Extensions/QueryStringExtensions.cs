using System.Reflection;

namespace Procoding.ApplicationTracker.Web;

public static class QueryStringExtensions
{
    public static string ToQueryString(this object obj)
    {
        if (obj == null)
            return string.Empty;

        var properties = obj.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(p => p.GetValue(obj, null) != null)
                            .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj, null).ToString())}");

        return string.Join("&", properties);
    }
}

using System.Collections;
using System.Reflection;
using System.Text;

namespace Procoding.ApplicationTracker.Web;

public static class QueryStringExtensions
{
    //public static string ToQueryString(this object obj)
    //{
    //    if (obj == null)
    //        return string.Empty;

    //    var properties = obj.GetType()
    //                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
    //                        .Where(p => p.GetValue(obj, null) != null)
    //                        .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj, null).ToString())}");

    //    return string.Join("&", properties);
    //}
    public static string ToQueryString(this object obj, string prefix = "")
    {
        if (obj == null)
            return string.Empty;

        var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetValue(obj, null) != null);

        var queryStringBuilder = new StringBuilder();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj, null);

            if (value is IList list && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    var itemProperties = item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(p => p.GetValue(item, null) != null);

                    foreach (var itemProperty in itemProperties)
                    {
                        queryStringBuilder.Append($"{prefix}{property.Name}[{i}].{itemProperty.Name}={Uri.EscapeDataString(itemProperty.GetValue(item).ToString())}&");
                    }
                }
            }
            else
            {
                queryStringBuilder.Append($"{prefix}{property.Name}={Uri.EscapeDataString(value.ToString())}&");
            }
        }

        return queryStringBuilder.ToString().TrimEnd('&');
    }
}

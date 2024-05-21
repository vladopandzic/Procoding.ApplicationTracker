using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Web.Extensions;

public static class ProblemDetailsExtensions
{
    public static List<string> ToErrors(this ProblemDetails problemDetails)
    {
        var errorsList = new List<string>();

        if (problemDetails.Extensions.TryGetValue("errors", out var errorsObject) &&
            errorsObject is JsonElement errorsElement &&
            errorsElement.ValueKind == JsonValueKind.Object)
        {
            try
            {
                var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText());

                if (errors != null)
                {
                    errorsList.AddRange(errors.SelectMany(x => x.Value));
                }
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializing 'errors': {jsonEx.Message}");
            }
        }

        return errorsList;
    }
}

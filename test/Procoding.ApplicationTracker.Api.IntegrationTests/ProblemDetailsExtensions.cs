using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests; 

public static class ProblemDetailsExtensions
{
    public static List<KeyValuePair<string, string[]>> GetErrors(this ProblemDetails problemDetails)
    {
        var errorsList = new List<KeyValuePair<string, string[]>>();

        if (problemDetails.Extensions.TryGetValue("errors", out var errorsObject) &&
            errorsObject is JsonElement errorsElement &&
            errorsElement.ValueKind == JsonValueKind.Object)
        {
            try
            {
                var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText());

                if (errors != null)
                {
                    errorsList.AddRange(errors);
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

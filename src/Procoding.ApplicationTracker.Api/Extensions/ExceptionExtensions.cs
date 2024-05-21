using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Procoding.ApplicationTracker.Api.Extensions;

public static class ExceptionExtensions
{
    public static ProblemDetails MapToResponse(this Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Status = 500,
            Detail = exception.Message,
            Instance = Guid.NewGuid().ToString()
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Status = 400;
            problemDetails.Title = "Validation Error";
            problemDetails.Extensions["errors"] = GetValidationErrors(validationException.Errors);
        }

        return problemDetails;
    }

    private static IDictionary<string, string[]> GetValidationErrors(IEnumerable<ValidationFailure> errors)
    {
        var errorDictionary = new Dictionary<string, string[]>();

        foreach (var error in errors)
        {
            if (errorDictionary.ContainsKey(error.PropertyName))
            {
                var existingErrors = errorDictionary[error.PropertyName];
                var newErrors = new List<string>(existingErrors) { error.ErrorMessage };
                errorDictionary[error.PropertyName] = newErrors.ToArray();
            }
            else
            {
                errorDictionary[error.PropertyName] = new[] { error.ErrorMessage };
            }
        }

        return errorDictionary;
    }
}
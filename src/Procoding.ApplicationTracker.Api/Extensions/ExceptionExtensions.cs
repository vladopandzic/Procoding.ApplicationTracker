using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Domain.Exceptions;

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
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Validation Error";
            problemDetails.Extensions["errors"] = GetValidationErrors(validationException.Errors);
        }

        if (exception is Unauthorized401Exception exception401)
        {
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Title = "Unauthorized exception";
            problemDetails.Extensions["errors"] = GetValidationErrors([new ValidationFailure("", exception401.Message)]);
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
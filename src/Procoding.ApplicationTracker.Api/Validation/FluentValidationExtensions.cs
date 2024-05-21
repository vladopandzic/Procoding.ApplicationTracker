using FluentValidation;

namespace Procoding.ApplicationTracker.Api.Validation;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                         .WithMessage("'{PropertyName}' is not a valid URL.");
    }
}

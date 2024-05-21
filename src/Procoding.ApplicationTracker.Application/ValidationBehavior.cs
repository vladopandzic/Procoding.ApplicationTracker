using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace Procoding.ApplicationTracker.Application;

public class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, Result<TResult>> where TRequest : notnull
{

    private readonly IValidator<TRequest> _validator;
    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<Result<TResult>> Handle(TRequest request, RequestHandlerDelegate<Result<TResult>> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<TResult>(new ValidationException(validationResult.Errors));
        }
        return await next();
    }
}

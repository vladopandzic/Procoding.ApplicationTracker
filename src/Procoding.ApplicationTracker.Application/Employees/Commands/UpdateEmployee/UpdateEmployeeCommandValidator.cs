using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;

internal sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator(IEmployeeRepository employeesRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.Employees.NameIsRequried);

        RuleFor(x => x.Surname).NotEmpty().WithError(ValidationErrors.Employees.SurnameIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.Employees.EmailIsRequired);

        RuleFor(x => x.Email).CustomAsync(async (email, validationContext, cancellationToken) =>
        {
            if (await employeesRepository.ExistsAsync(email, validationContext.InstanceToValidate.Id, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.Employees.EmailAlreadyExists.Message);
            }
        });
    }
}


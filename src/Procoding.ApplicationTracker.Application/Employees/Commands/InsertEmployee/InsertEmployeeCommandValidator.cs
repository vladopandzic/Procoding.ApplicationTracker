using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.InsertEmployee;

internal sealed class InsertEmployeeCommandValidator : AbstractValidator<InsertEmployeeCommand>
{
    public InsertEmployeeCommandValidator(IEmployeeRepository employeeRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.Employees.NameIsRequried)
                            .MaximumLength(Employee.MaxLengthForName)
                            .MinimumLength(Employee.MinLengthForName);

        RuleFor(x => x.Surname).NotEmpty().WithError(ValidationErrors.Employees.SurnameIsRequired)
                               .MaximumLength(Employee.MaxLengthForSurname)
                               .MinimumLength(Employee.MinLengthForSurname);

        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.Employees.EmailIsRequired);

        RuleFor(x => x.Email).CustomAsync(async (email, validationContext, cancellationToken) =>
        {
            if (await employeeRepository.ExistsAsync(email, Guid.Empty, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.Employees.EmailAlreadyExists.Message);
            }
        });
    }

}

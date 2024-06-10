using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.InsertEmployee;

internal sealed class InsertEmployeeCommandHandler : ICommandHandler<InsertEmployeeCommand, EmployeeInsertedResponseDTO>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Employee> _passwordHasher;

    public InsertEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IPasswordHasher<Employee> passwordHasher)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<EmployeeInsertedResponseDTO>> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var email = new Email(request.Email);


        var employee = Employee.Create(id, name: request.Name, surname: request.Surname, email: email, password: request.Password, _passwordHasher);


        var result = await _employeeRepository.InsertAsync(employee, request.Password, cancellationToken);

        if (!result.Succeeded)
        {
            return new Result<EmployeeInsertedResponseDTO>(new ValidationException(result.Errors.Select(x => new FluentValidation.Results.ValidationFailure("",
                                                                                                                                                            x.Description))));
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var employeeDto = new EmployeeDTO(id: employee.Id,
                                          name: employee.Name,
                                          surname: employee.Surname,
                                          email: employee.Email.Value);

        return new EmployeeInsertedResponseDTO(employeeDto);
    }
}

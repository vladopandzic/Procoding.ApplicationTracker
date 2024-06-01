using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;

internal sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, EmployeeUpdatedResponseDTO>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EmployeeUpdatedResponseDTO>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeAsync(request.Id, cancellationToken);

        //TODO: use result object
        if (employee is null)
        {
            throw new EmployeeDoesNotExistException("Employee does not exist");
        }

        var email = new Email(request.Email);

        employee.Update(request.Name, request.Surname, email);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var employeeDto = new EmployeeDTO(employee.Id, name: employee.Name, surname: employee.Surname, email: employee.Email.Value);

        return new EmployeeUpdatedResponseDTO(employeeDto);
    }
}

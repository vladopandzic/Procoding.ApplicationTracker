using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Queries.GetOneEmployee;

internal sealed class GetOneEmployeeQueryHandler : IQueryHandler<GetOneEmployeeQuery, EmployeeResponseDTO>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetOneEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeResponseDTO> Handle(GetOneEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeAsync(request.Id, cancellationToken);

        if (employee is null)
            throw new Domain.Exceptions.EmployeeDoesNotExistException();

        var employeeDto = new EmployeeDTO(id: employee.Id,
                                         name: employee.Name,
                                         surname: employee.Surname,
                                         email: employee.Email.Value);

        return new EmployeeResponseDTO(employeeDto);
    }
}

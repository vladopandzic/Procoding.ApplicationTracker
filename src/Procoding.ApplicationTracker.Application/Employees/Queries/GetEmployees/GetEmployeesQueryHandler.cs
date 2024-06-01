using Procoding.ApplicationTracker.Application.Employees.Queries.GetEmployees;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Employees.Queries.GetEmployees;

internal sealed class GetEmployeeQueryHandler : IQueryHandler<GetEmployeesQuery, EmployeeListResponseDTO>
{
    private readonly IEmployeeRepository _EmployeeRepository;

    public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _EmployeeRepository = employeeRepository;
    }

    public async Task<EmployeeListResponseDTO> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var specification = new Specifications.EmployeeGetListSpecification(request.PageNumber, request.PageSize, request.Filters, request.Sort);

        var employees = await _EmployeeRepository.GetEmployeesAsync(specification, cancellationToken);

        var count = await _EmployeeRepository.CountAsync(specification, cancellationToken);

        var EmployeeDTOs = employees.Select(x => new EmployeeDTO(x.Id, x.Name, x.Surname, x.Email.Value)).ToList();

        return new EmployeeListResponseDTO(employees: EmployeeDTOs.AsReadOnly(), totalCount: count);
    }
}

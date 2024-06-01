using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Queries.GetOneEmployee;

public sealed class GetOneEmployeeQuery : IQuery<EmployeeResponseDTO>
{
    public GetOneEmployeeQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Employees.Queries.GetOneEmployee;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Api.Endpoints.Employees;

public class GetOneEmployeeEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithResult<EmployeeResponseDTO>
{
    readonly ISender _sender;
    public GetOneEmployeeEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("employees/{id}")]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.EmployeeOnly)]
    public override Task<EmployeeResponseDTO> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetOneEmployeeQuery(id), cancellationToken);
    }
}

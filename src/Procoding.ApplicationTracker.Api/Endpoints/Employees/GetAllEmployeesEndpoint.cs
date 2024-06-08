using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Core.Query;
using Procoding.ApplicationTracker.Application.Employees.Queries.GetEmployees;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Api.Endpoints.Employees;

public class GetAllEmployeesEndpoint : EndpointBaseAsync.WithRequest<EmployeeGetListRequestDTO>.WithResult<EmployeeListResponseDTO>
{
    readonly ISender _sender;
    public GetAllEmployeesEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("employees")]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.EmployeeOnly)]
    public override Task<EmployeeListResponseDTO> HandleAsync([FromQuery] EmployeeGetListRequestDTO request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetEmployeesQuery(pageNumber: request.PageNumber,
                                                   pageSize: request.PageSize,
                                                   filters: request.Filters.Select(x => new Filter()
                                                   {
                                                       Key = x.Key,
                                                       Operator = x.Operator,
                                                       Value = x.Value
                                                   }).ToList(),
                                                   sort: request.Sort.Select(x => new Sort()
                                                   {
                                                       SortBy = x.SortBy,
                                                       Descending = x.Descending
                                                   }).ToList()), cancellationToken);
    }
}

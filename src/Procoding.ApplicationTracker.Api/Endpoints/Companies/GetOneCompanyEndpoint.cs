using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Companies.Queries.GetCompanies;
using Procoding.ApplicationTracker.Application.Companies.Queries.GetOneCompany;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class GetOneCompanyEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithResult<CompanyResponseDTO>
{
    readonly ISender _sender;
    public GetOneCompanyEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("companies/{id}")]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate")]
    public override Task<CompanyResponseDTO> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetOneCompanyQuery(id), cancellationToken);
    }
}

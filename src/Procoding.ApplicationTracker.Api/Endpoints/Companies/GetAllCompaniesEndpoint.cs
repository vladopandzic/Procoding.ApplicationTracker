using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Companies.Queries.GetCompanies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class GetAllCompaniesEndpoint : EndpointBaseAsync.WithoutRequest.WithResult<CompanyListResponseDTO>
{
    readonly ISender _sender;
    public GetAllCompaniesEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("companies")]
    public override Task<CompanyListResponseDTO> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetCompaniesQuery(), cancellationToken);
    }
}

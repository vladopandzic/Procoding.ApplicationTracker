using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;
using Procoding.ApplicationTracker.Application.Companies.Queries.GetOneCompany;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class InsertCompanyEndpoint : EndpointBaseAsync.WithRequest<InsertCompanyCommand>.WithResult<CompanyInsertedResponseDTO>
{
    readonly ISender _sender;
    public InsertCompanyEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("companies")]
    public override Task<CompanyInsertedResponseDTO> HandleAsync(InsertCompanyCommand request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(request, cancellationToken);
    }
}

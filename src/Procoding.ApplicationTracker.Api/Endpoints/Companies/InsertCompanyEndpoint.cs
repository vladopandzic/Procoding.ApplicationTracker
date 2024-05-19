using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class InsertCompanyEndpoint : EndpointBaseAsync.WithRequest<CompanyInsertRequestDTO>.WithResult<CompanyInsertedResponseDTO>
{
    readonly ISender _sender;
    public InsertCompanyEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("companies")]
    public override Task<CompanyInsertedResponseDTO> HandleAsync(CompanyInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new InsertCompanyCommand(request.Name, request.OfficialWebSiteLink), cancellationToken);
    }
}

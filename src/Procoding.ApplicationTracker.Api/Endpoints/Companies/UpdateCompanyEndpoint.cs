﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class UpdateCompanyEndpoint : EndpointBaseAsync.WithRequest<CompanyUpdateRequestDTO>.WithResult<CompanyUpdatedResponseDTO>
{
    readonly ISender _sender;

    public UpdateCompanyEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("companies")]

    public override Task<CompanyUpdatedResponseDTO> HandleAsync(CompanyUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new UpdateCompanyCommand(request.Id, request.Name, request.OfficialWebSiteLink), cancellationToken);
    }
}
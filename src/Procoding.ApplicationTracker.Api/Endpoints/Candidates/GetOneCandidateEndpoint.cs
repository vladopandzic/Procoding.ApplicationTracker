﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api;

public class GetOneCandidateEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithResult<CandidateResponseDTO>
{
    readonly ISender _sender;
    public GetOneCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("candidates/{id}")]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.EmployeeOnly)]
    public override Task<CandidateResponseDTO> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetOneCandidateQuery(id), cancellationToken);
    }
}
